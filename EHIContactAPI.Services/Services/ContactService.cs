using EHIContactAPI.Providers;
using EHIContactAPI.Providers.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHIContactAPI.Services.Services
{
    public class ContactService : IContactService
    {
        IContactDataProvider _contactProvider;
        public ContactService(IContactDataProvider contactDataProvider)
        {
            _contactProvider = contactDataProvider;
        }
        public async Task<Contacts> AddNewContact(Contacts contact)
        {
           return await _contactProvider.AddNewContact(contact);
        }

        public async Task<bool> DeleteContact(long contactId)
        {
            return await _contactProvider.DeleteContact(contactId); 
        }

        public async Task<List<Contacts>> GetAllContacts()
        {
            var list = await _contactProvider.GetAllContacts();
            
            //Retrun only Active Contacts
            if(list != null)
            {
                return list.Where(c => c.Status == CustomerStatus.Active).ToList();
            }

            return list;            
        }

        public async Task<Contacts> UpdateContact(long contactId, Contacts contact)
        {
            return await _contactProvider.UpdateContact(contactId, contact);
        }
    }
}
