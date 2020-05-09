using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EHIContactAPI.Controllers
{
    [Route("~/HealthCheck")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Server Status: OK");
            
        }
    }
}