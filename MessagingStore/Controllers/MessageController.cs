using MessagingStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MessagingStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : Controller
    {
        [HttpPost("Send")]
        public IActionResult Send(MessageDetail message)
        {
            if (message != null) return Ok(message);

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
