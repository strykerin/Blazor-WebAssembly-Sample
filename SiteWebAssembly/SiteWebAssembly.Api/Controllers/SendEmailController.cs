using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiteWebAssembly.Api.Services;
using SiteWebAssembly.Model;

namespace SiteWebAssembly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailController : ControllerBase
    {
        private readonly ILogger<SendEmailController> _logger;
        private readonly ISendEmailService _sendEmailService;

        public SendEmailController(ILogger<SendEmailController> logger, ISendEmailService sendEmailService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sendEmailService = sendEmailService ?? throw new ArgumentNullException(nameof(sendEmailService));
        }

        [HttpPost("contact")]
        public async Task<ActionResult> SendEmail ([FromBody] Contact contact)
        {
            try
            {
                if (string.IsNullOrEmpty(contact.Email) || string.IsNullOrEmpty(contact.Message) || string.IsNullOrEmpty(contact.Name))
                {
                    _logger.LogDebug("Validation Error");
                    return BadRequest();
                }

                bool response = await _sendEmailService.SendEmail(contact);
                if (response)
                {
                    return Ok();
                }
                else
                {
                    _logger.LogError($"Error in {nameof(_sendEmailService)} service");
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
