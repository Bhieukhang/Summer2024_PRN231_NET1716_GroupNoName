using JSS_BusinessObjects.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IHubContext<SignalRServer> _hubContext;

        public MessageController(IHubContext<SignalRServer> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] MessageModel message)
        {
            if (string.IsNullOrEmpty(message.User) || string.IsNullOrEmpty(message.Message))
            {
                return BadRequest("User and Message are required.");
            }

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message.User, message.Message);
            return Ok(new { Status = "Message sent successfully." });
        }
    }

    public class MessageModel
    {
        public string User { get; set; }
        public string Message { get; set; }
    }
}