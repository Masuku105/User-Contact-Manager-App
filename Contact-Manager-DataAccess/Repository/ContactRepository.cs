using Contact_Manager_DataAccess.Interfaces;
using Contact_Manager_DataAccess.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Manager_DataAccess.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly IMongoCollection<Contact> _contactCollection;
        public ContactRepository(IOptions<ContactDatabaseSettings> contactDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            contactDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                contactDatabaseSettings.Value.DatabaseName);

            _contactCollection = mongoDatabase.GetCollection<Contact>(
                contactDatabaseSettings.Value.ContactCollection);

        }


        public async Task<List<Contact>> GetAsync() =>
            await _contactCollection.Find(_ => true).ToListAsync();

        public async Task<Contact?> GetAsync(string id) =>
            await _contactCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Contact newContact) =>
            await _contactCollection.InsertOneAsync(newContact);

        [Obsolete]
        public async Task UpdateAsync(Contact updatedContact) =>
            await _contactCollection.ReplaceOneAsync(w => w.Id == updatedContact.Id,
          updatedContact, new UpdateOptions { IsUpsert = true });

        public async Task RemoveAsync(string id) =>
        await _contactCollection.DeleteOneAsync(x => x.Id == id);
    }
}
