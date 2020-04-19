using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Chat
{
    public class Chat
    {
        public Chat()
        {
            MessageList = new List<string>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string IdFirstUser { get; set; }
        public string IdSecondUser { get; set; }
        public List<string> MessageList { get; set; }
    }
}
