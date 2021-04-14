using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIS.ViewModels.Meeting
{
    public class ChatViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime End { get; set; }
        public string Topic { get; set; }
    }
}
