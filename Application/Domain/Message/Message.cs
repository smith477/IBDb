using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Message
{
    public class Message
    {
        public string Id { get; set; }
        public string MessageText { get; set; }
        public string DateOfMessage { get; set; }
        public string SenderId { get; set; }
        public string ChatId { get; set; }
    }
}
