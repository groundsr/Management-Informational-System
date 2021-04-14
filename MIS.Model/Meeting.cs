using System;
using System.Collections.Generic;
using System.Text;

namespace MSI.Model
{
    public class Meeting
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Topic { get; set; }
    }
}
