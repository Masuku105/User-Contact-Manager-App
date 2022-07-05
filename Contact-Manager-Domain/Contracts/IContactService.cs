using Contact_Manager_Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Manager_Domain.Contracts
{
    public interface IContactService
    {
        Task<List<ContactDTO>> GetAsync();
        Task<ContactDTO?> GetAsync(string id);
        Task CreateAsync(ContactDTO newContact);
        Task UpdateAsync(ContactDTO updatedContact);
        Task RemoveAsync(string id);
    }
}
