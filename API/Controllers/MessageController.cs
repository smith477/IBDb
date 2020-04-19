using Microsoft.AspNetCore.Mvc;
using Application.DataProvider;
using Application.Domain.Message;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetMessagesById(string id)
        {
            return Ok(MessageDataProvider.GetMessagesByChatID(id));
        }
        [HttpGet("messagebydate")]
        public IActionResult GetMessagesByDate(Message msg)
        {
            return Ok(MessageDataProvider.GetMessagesByDateTimeAndChatID(msg));
        }
        [HttpPut]
        public IActionResult PutMessage(Message msg)
        {
            MessageDataProvider.AddMessage(msg);
            return Ok();
        }
        [HttpPut]
        public IActionResult ChangeMessage(Message msg)
        {
            MessageDataProvider.ChangeMessageText(msg);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(string id)
        {
            MessageDataProvider.DeleteMessageByID(id);
            return Ok();
        }
    }
}