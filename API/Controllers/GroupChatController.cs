using Application.DataProvider;
using Application.Domain.Chat;
using Application.Domain.User;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupChatController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllGroupChats()
        {
            return Ok(GroupChatDataProvider.GetGroupChats());
        }
        [HttpGet("usergroups/{id}")]
        public IActionResult GetByUserId(string id)
        {
            return Ok(GroupChatDataProvider.GetGroupChatsByUserID(id));
        }
        [HttpGet("group/{id}")]
        public IActionResult GetByGroupId(string id)
        {
            return Ok(GroupChatDataProvider.GetGroupChatByID(id));
        }
        [HttpPut("creategroupchat")]
        public IActionResult PutGroupChat(JObject obj)
        {
            GroupChat groupChat = obj["groupchat"].ToObject<GroupChat>();
            User user = obj["user"].ToObject<User>();
            GroupChatDataProvider.AddGroupChat(groupChat, user);
            return Ok();
        }
        [HttpPut("adduser")]
        public IActionResult PutGroupChatUser(JObject obj)
        {
            GroupChat groupChat = obj["groupchat"].ToObject<GroupChat>();
            User user = obj["user"].ToObject<User>();
            GroupChatDataProvider.AddGroupChatUser(groupChat, user);
            return Ok();
        }
        [HttpPut("removeuser")]
        public IActionResult RemoveGroupChatUser(JObject obj) 
        {
            GroupChat groupChat = obj["groupchat"].ToObject<GroupChat>();
            User user = obj["user"].ToObject<User>();
            GroupChatDataProvider.RemoveGroupChatUserID(groupChat, user);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteGroupChat(string id)
        {
            GroupChatDataProvider.DeleteGroupChat(id);
            return Ok();
        }
    }
}