using EHIContactAPI.Providers.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHIContactAPI.Providers.Providers
{
    public class InMemoryContactDataProvider : IContactDataProvider
    {
        private const string ContactCacheKey = "ContactCache";
        private readonly MemoryCache _cache;
        public InMemoryContactDataProvider()
        {
            _cache = new MemoryCache(new MemoryCacheOptions { SizeLimit = 1000 });
            AddInitialDataToCache();
        }
        public Task<Contacts> AddNewContact(Contacts contact)
        {
            var contacts = _cache.Get<List<Contacts>>(ContactCacheKey);

            contacts ??= new List<Contacts>();

            contact.ContactId = new Random().Next();
            contacts.Add(contact);

            AddToCache(contacts);

            return Task.FromResult(contact);
        }

        public Task<bool> DeleteContact(long contactId)
        {
            var contacts = _cache.Get<List<Contacts>>(ContactCacheKey);

            if(contacts !=null)
            {
                var contact = contacts.FirstOrDefault(c => c.ContactId == contactId);
                if(contact !=null)
                {
                    contact.Status = CustomerStatus.InActive;
                    AddToCache(contacts);
                    return Task.FromResult(true);
                }
            }

            return Task.FromResult(false);
        }

        public Task<List<Contacts>> GetAllContacts()
        {
            var list =  _cache.Get<List<Contacts>>(ContactCacheKey);

            return Task.FromResult(list);
        }

        public Task<Contacts> UpdateContact(long contactId, Contacts contact)
        {
            var list = _cache.Get<List<Contacts>>(ContactCacheKey);

            var existingContact = list.Where(c => c.ContactId == contactId).FirstOrDefault();
            
            //Update only fields which are passed in the payload
            if(existingContact != null)
            {
                existingContact.Email = contact.Email ?? existingContact.Email;
                existingContact.FirstName = contact.FirstName ?? existingContact.FirstName;
                existingContact.LastName = contact.LastName ?? existingContact.LastName;
                existingContact.PhoneNumber = contact.PhoneNumber > 0 ? contact.PhoneNumber : existingContact.PhoneNumber;
                AddToCache(list);
                return Task.FromResult(existingContact);
            }

            throw new Exception("Contact could not found");           

        }

        private void AddToCache(List<Contacts> list)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSize(1)                
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));
    
            _cache.Set<List<Contacts>>(ContactCacheKey, list, cacheEntryOptions);
        }

        private void AddInitialDataToCache()
        {
            var list = new List<Contacts>
                {
                   new Contacts
                   {
                       ContactId =1,
                       Email = "john.b@ehi.com",
                       FirstName ="John",
                       LastName = "Boner",
                       PhoneNumber = 730456000,
                       Status = CustomerStatus.Active
                   },
                   new Contacts
                   {
                       ContactId =2,
                       Email = "james.b@ehi.com",
                       FirstName ="James",
                       LastName = "Berry",
                       PhoneNumber = 730489000,
                       Status = CustomerStatus.Active
                   },
                   new Contacts
                   {
                       ContactId =3,
                       Email = "merry.j@ehi.com",
                       FirstName ="merry",
                       LastName = "john",
                       PhoneNumber = 730489000,
                       Status = CustomerStatus.InActive
                   }
                };

            AddToCache(list);
        }
    }
}
