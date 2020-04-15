using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Chat
{
    class GroupChat
    {
        public GroupChat()
        {
            UserList = new List<string>();
            MessageList = new List<string>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public List<string> UserList { get; set; }
        public List<string> MessageList { get; set; }
    }
}
