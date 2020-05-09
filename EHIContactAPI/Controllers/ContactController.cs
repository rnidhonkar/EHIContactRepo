using EHIContactAPI.Providers.Models;
using EHIContactAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EHIContactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactController> _logger;

        public ContactController(ILogger<ContactController> logger, IContactService contactService)
        {
            _logger = logger;
            _contactService = contactService;
        }

        [HttpGet("getall")]
        //[Authorize] TO DO : Add authorization
        public async Task<IActionResult> GetAllContacts()
        {
            try
            {
                var result = await _contactService.GetAllContacts();
                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occured while getting Contacts.", ex);
                return BadRequest();
            }
        }

        [HttpPost("addnew")]
        public async Task<IActionResult> AddNewContact([FromBody] Contacts contact)
        {
            try
            {
                var result = await _contactService.AddNewContact(contact);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while adding new Contact.", ex);
                return BadRequest();
            }
        }

        [HttpPatch("delete")]
        public async Task<IActionResult> DeleteContact([FromBody] long contactId)
        {
            try
            {
                if(await _contactService.DeleteContact(contactId))
                {
                    return Ok("Contact deleted successfully");
                }

                return NotFound("Contact not found");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while deleting Contact.", ex);
                return BadRequest();
            }
        }

        [HttpPatch("update/{contactId}")]
        public async Task<IActionResult> UpdateContact([FromRoute] long contactId, [FromBody] Contacts contact)
        {
            try
            {
                var result = await _contactService.UpdateContact(contactId, contact);

                return Ok(result);
                
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while updating Contact.", ex);
                return BadRequest();
            }
        }
    }

}