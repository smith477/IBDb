using System;
using System.Collections.Generic;

namespace Application.Domain.User
{
    public class User
    {
        public User()
        {
            ChatListId = new List<string>();
            GroupChatListId = new List<string>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Birthday { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> ChatListId { get; set; }
        public List<string> GroupChatListId { get; set; }
    }
}