using AutoMapper;
using Contact_Manager_DataAccess.Interfaces;
using Contact_Manager_DataAccess.Models;
using Contact_Manager_Domain.Contracts;
using Contact_Manager_Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Manager_Domain.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _objectMapper;

        public ContactService(IContactRepository contactRepository, IMapper objectMapper)
        {
            _contactRepository = contactRepository;
            _objectMapper = objectMapper;
        }
        public async Task<List<ContactDTO>> GetAsync()
        {
            var contact = await _contactRepository.GetAsync();
            return _objectMapper.Map<List<ContactDTO>>(contact);
        }

        public async Task<ContactDTO?> GetAsync(string id)
        {
            var contact = await _contactRepository.GetAsync(id);
            return _objectMapper.Map<ContactDTO>(contact);
        }
        public async Task CreateAsync(ContactDTO newContact)
        {
            await _contactRepository.CreateAsync(_objectMapper.Map<Contact>(newContact));
        }
        public async Task UpdateAsync( ContactDTO updatedContact)
        {
            await _contactRepository.UpdateAsync(_objectMapper.Map<Contact>(updatedContact));
        }
        public async Task RemoveAsync(string id)
        {
            await _contactRepository.RemoveAsync(id);
        }
    }
}
