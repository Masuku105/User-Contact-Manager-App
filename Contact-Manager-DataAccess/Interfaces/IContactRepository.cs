using Contact_Manager_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Manager_DataAccess.Interfaces
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAsync();
        Task<Contact?> GetAsync(string id);
        Task CreateAsync(Contact newContact);
        Task UpdateAsync(Contact updateContact);
        Task RemoveAsync(string id);
    }
}
