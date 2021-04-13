using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.SignalR
{
    public class MessageDTO
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
    }
}
