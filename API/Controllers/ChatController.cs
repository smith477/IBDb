using Application.DataProvider;
using Application.Domain.Chat;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetChats() 
        {
            return Ok(ChatDataProvider.GetAllChats());
        }
        [HttpGet("userchats/{id}")]
        public IActionResult GetByUserId(string id) 
        {
            return Ok(ChatDataProvider.GetChatsByUserID(id));
        }
        [HttpGet("messages/{id}")]
        public IActionResult GetByChatId(string id)
        {
            return Ok(ChatDataProvider.GetChatByID(id));
        }
        [HttpPut]
        public IActionResult PutChat(Chat chat)
        {
            ChatDataProvider.AddChat(chat);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteChat(string id)
        {
            ChatDataProvider.DeleteChatByID(id);
            return Ok();
        }
    }
}