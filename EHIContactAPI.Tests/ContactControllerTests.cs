using EHIContactAPI.Controllers;
using EHIContactAPI.Providers.Models;
using EHIContactAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace EHIContactAPI.Tests
{
    public class ContactControllerTests
    {
        private ContactController contactController;
        private readonly Mock<IContactService> _contactService;
        private readonly Mock<ILogger<ContactController>> _logger;

        public ContactControllerTests()
        {
            _contactService = new Mock<IContactService>();
            _logger = new Mock<ILogger<ContactController>>();
            contactController = new ContactController(_logger.Object, _contactService.Object);
        }

        [Test]
        public void Contact_GetAllContact_Success()
        {
            _contactService.Setup(obj => obj.GetAllContacts()).ReturnsAsync(GetListOfContacts);
            var result = contactController.GetAllContacts().Result as OkObjectResult;
           
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);            
        }

        [Test]
        public void Contact_GetAllContact_Failure()
        {
            _contactService.Setup(obj => obj.GetAllContacts()).ThrowsAsync(new Exception("test exception"));

            var result = contactController.GetAllContacts().Result as BadRequestResult;
            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }


        [Test]
        public void Contact_AddNewContact_Success()
        {
            _contactService.Setup(obj => obj.AddNewContact(It.IsAny<Contacts>())).ReturnsAsync(GetContact);
            
            var result = contactController.AddNewContact(GetContact()).Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void Contact_AddNewContact_Failure()
        {
            _contactService.Setup(obj => obj.AddNewContact(It.IsAny<Contacts>())).ThrowsAsync(new Exception("test exception"));
            
            var result = contactController.AddNewContact(GetContact()).Result as BadRequestResult;

            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void Contact_DeleteContact_Success()
        {
            _contactService.Setup(obj => obj.DeleteContact(It.IsAny<long>())).ReturnsAsync(true);

            var result = contactController.DeleteContact(1234).Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void Contact_DeleteContact_NotFound()
        {
            _contactService.Setup(obj => obj.DeleteContact(It.IsAny<long>())).ReturnsAsync(false);

            var result = contactController.DeleteContact(1234).Result as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }


        [Test]
        public void Contact_DeleteContact_Failure()
        {
            _contactService.Setup(obj => obj.DeleteContact(It.IsAny<long>())).ThrowsAsync(new Exception("test exception"));

            var result = contactController.DeleteContact(1234).Result as BadRequestResult;

            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void Contact_UpdateContact_Success()
        {
            _contactService.Setup(obj => obj.UpdateContact(It.IsAny<long>(), It.IsAny<Contacts>())).ReturnsAsync(GetContact);

            var result = contactController.UpdateContact(1234,GetContact()).Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void Contact_UpdateContact_Failure()
        {
            _contactService.Setup(obj => obj.UpdateContact(It.IsAny<long>(), It.IsAny<Contacts>())).ThrowsAsync(new Exception("test exception"));

            var result = contactController.UpdateContact(1234, GetContact()).Result as BadRequestResult;

            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }
        private List<Contacts> GetListOfContacts()
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
                   }
            };
        }

        private Contacts GetContact()
        {
            return new Contacts
            {
                ContactId = 1,
                Email = "john.b@ehi.com",
                FirstName = "John",
                LastName = "Boner",
                PhoneNumber = 730456000,
                Status = CustomerStatus.Active
            };            
        }
    }
}