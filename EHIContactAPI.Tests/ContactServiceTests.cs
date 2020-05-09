using EHIContactAPI.Providers;
using EHIContactAPI.Providers.Models;
using EHIContactAPI.Services.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace EHIContactAPI.Tests
{
    public class ContactServiceTests
    {
        private readonly Mock<IContactDataProvider> _contactProvider;
        private ContactService contactService;
        public ContactServiceTests()
        {
            _contactProvider = new Mock<IContactDataProvider>();
            contactService = new ContactService(_contactProvider.Object);
        }

        [Test]
        public void ContactService_GetAllContact_ReturnsActiveContactOnly()
        {
            _contactProvider.Setup(obj => obj.GetAllContacts()).ReturnsAsync(GetAllContacts);
            var result = contactService.GetAllContacts().Result;

            Assert.NotNull(result);
            //Inactive should not come
            Assert.AreEqual(1, result.Count);
        }

        private List<Contacts> GetAllContacts()
        {
            return new List<Contacts>
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
                       ContactId =3,
                       Email = "merry.j@ehi.com",
                       FirstName ="merry",
                       LastName = "john",
                       PhoneNumber = 730489000,
                       Status = CustomerStatus.InActive
                }

            };
        }
    }
}
