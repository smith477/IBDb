using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Message
{
    class Message
    {
        public string Id { get; set; }
        public string MessageText { get; set; }
        public DateTime DateOfMessage { get; set; }
        public string SenderId { get; set; }
    }
}
