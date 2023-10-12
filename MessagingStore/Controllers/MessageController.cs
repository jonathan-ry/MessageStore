using MessagingStore.Interfaces;
using MessagingStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MessagingStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send(MessageDetail message)
        {
            if (message != null)
            {
                await _messageService.Send(message);
                return Ok(message);
            }

            return BadRequest();
        }

        [HttpGet("Get/{id}")]
        public IActionResult Get(int id) 
        {
            MessageDetail message = new MessageDetail
            {
                Id = id,
                Sender = "Accenture Team",
                Recipient = "Juan Dela Cruz",
                Message = "Welcome to Accenture, Juan Dela Cruz"
            };
            
            return Ok(message);

        }
    }
}
