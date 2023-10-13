using MessagingStore.Interfaces;
using MessagingStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

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
        public async Task<IActionResult> Send(int num)
        {
            var response = GetErrorResponse(num);
            try
            {
                if (response != null)
                {
                    await _messageService.Send(response);
                    return Ok(response);
                }

                return BadRequest("Empty Response");
            }
            catch(Exception ex) 
            {
                return BadRequest($"Bad Request: {ex}");
            }
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

        protected ErrorResponse GetErrorResponse(int id) 
        {
            switch(id) 
            {
                //Unauthorized Access Error
                case 1:
                    return new ErrorResponse
                    {
                        ErrorId = Guid.NewGuid(),
                        Timestamp = DateTime.Now,
                        HttpStatusCode = 401,
                        Title = "Unauthorized",
                        Message = "Access denied. You do not have permission to view this resource.",
                        Location = "Security_Repository",
                        RequestInfo = new RequestInfo()
                        {
                            User = "Juan",
                            RequestId = "123456789"
                        }
                    };
                //Server Error
                case 2:
                    return new ErrorResponse
                    {
                        ErrorId = Guid.NewGuid(),
                        Timestamp = DateTime.Now,
                        HttpStatusCode = 500,
                        Title = "Internal Server Error",
                        Message = "An unexpected server error occurred while processing your request.",
                        Location = "Database_Repository",
                        RequestInfo = new RequestInfo()
                        {
                            User = "Bob",
                            RequestId = "123456789"
                        }
                    };
                //Validation  Error
                case 3:
                    return new ErrorResponse
                    {
                        ErrorId = Guid.NewGuid(),
                        Timestamp = DateTime.Now,
                        HttpStatusCode = 400,
                        Title = "Bad Request",
                        Message = "Validation failed. Please check the input data for errors.",
                        Location = "Login_Repository",
                        RequestInfo = new RequestInfo()
                        {
                            User = "Tom",
                            RequestId = "123456789"
                        }
                    };
                //System Failed
                case 4:
                    return new ErrorResponse
                    {
                        ErrorId = Guid.NewGuid(),
                        Timestamp = DateTime.Now,
                        HttpStatusCode = 500,
                        Title = "Internal Server Error",
                        Message = "A critical system failure has occurred. The application is not operational.",
                        Location = "System_Repository",
                        RequestInfo = new RequestInfo()
                        {
                            User = "Admin",
                            RequestId = "123456789"
                        }
                    };
                //Validation  Error
                default:
                    return new ErrorResponse
                    {
                        ErrorId = Guid.NewGuid(),
                        Timestamp = DateTime.Now,
                        HttpStatusCode = 409,
                        Title = "Conflict",
                        Message = "A conflict occurred when attempting to update the resource.",
                        Location = "Resource_Repository",
                        RequestInfo = new RequestInfo()
                        {
                            User = "Admin",
                            RequestId = "123456789"
                        }
                    };
            }
        }
    }
}
