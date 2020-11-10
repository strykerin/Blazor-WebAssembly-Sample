using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiteWebAssembly.Model;

namespace SiteWebAssembly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailController : ControllerBase
    {
        private readonly ILogger<SendEmailController> _logger;

        public SendEmailController(ILogger<SendEmailController> logger)
        {
            _logger = logger;
        }

        [HttpPost("contact")]
        public async Task SendEmail ([FromBody] Contact contact)
        {
            if (string.IsNullOrEmpty(contact.Email) || string.IsNullOrEmpty(contact.Message) || string.IsNullOrEmpty(contact.Name))
            {

            }
        }
    }
}
