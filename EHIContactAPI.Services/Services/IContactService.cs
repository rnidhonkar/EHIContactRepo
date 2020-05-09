using EHIContactAPI.Providers.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EHIContactAPI.Services
{
    public interface IContactService
    {
        Task<List<Contacts>> GetAllContacts();
        Task<Contacts> AddNewContact(Contacts contact);
        Task<Contacts> UpdateContact(long contactId, Contacts contact);
        Task<bool> DeleteContact(long contactId);
    }
}
