using EHIContactAPI.Providers.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EHIContactAPI.Providers.Providers
{
    public class SQLContactDataProvider : IContactDataProvider
    {
        public Task<Contacts> AddNewContact(Contacts contact)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteContact(long contactId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Contacts>> GetAllContacts()
        {
            throw new NotImplementedException();
        }

        public Task<Contacts> UpdateContact(long contactId, Contacts contact)
        {
            throw new NotImplementedException();
        }
    }
}
