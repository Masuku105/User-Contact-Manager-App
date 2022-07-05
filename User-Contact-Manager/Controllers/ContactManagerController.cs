using AutoMapper;
using Contact_Manager_Domain.Contracts;
using Contact_Manager_Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using User_Contact_Manager.Models;
using System.Linq;
using User_Contact_Manager.Helpers;

namespace User_Contact_Manager.Controllers
{
    public class ContactManagerController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IMapper _objectMapper;

        public ContactManagerController(IContactService contactService, IMapper objectMapper)
        {
            _contactService = contactService;
            _objectMapper = objectMapper;
        }
        public async Task<IActionResult> Index()
        {
            var contacts = await _contactService.GetAsync();
            var userContacts = _objectMapper.Map<List<ContactViewModel>>(contacts);
            return userContacts is not null ? View(userContacts) : Problem("You don't have any contact");
        }

        public async Task<IActionResult> AddOrEdit(string? id = null)
        {
            if (id == null)
            {
                return View(new ContactViewModel());
            }
            else
            {
                var contact = await _contactService.GetAsync(id);
                var contactViewModel = _objectMapper.Map<ContactViewModel>(contact);
                if (contactViewModel == null)
                {
                    return NotFound();
                }
                return View(contactViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("Id,Name,Phone,Email,Company,Notes")] ContactViewModel contact)
        {

            if (ModelState.IsValid)
            {
                //Insert
                if (contact.Id == null)
                {
                    await _contactService.CreateAsync(_objectMapper.Map<ContactDTO>(contact));   
                }
                //Update
                else
                {
                    try
                    {                       
                        await _contactService.UpdateAsync(_objectMapper.Map<ContactDTO>(contact));                       
                    }
                    catch (Exception ex)
                    {
                        if (!await ContactExist(contact.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw ex; 
                        }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _objectMapper.Map<List<ContactViewModel>>(await _contactService.GetAsync())) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", contact) });
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _contactService.GetAsync(id);
               
            if (contact == null)
            {
                return NotFound();
            }

            return View(_objectMapper.Map<ContactViewModel>(contact));
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            await _contactService.RemoveAsync(id);
            var contacts = await _contactService.GetAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _objectMapper.Map<List<ContactViewModel>>(contacts)) });
        }
        private async Task<bool> ContactExist(string id)
        {
            var contacts = await _contactService.GetAsync();
            return (contacts?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
