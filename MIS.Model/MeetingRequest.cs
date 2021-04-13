using System;
using System.Collections.Generic;
using System.Text;

namespace MSI.Model
{
    public class MeetingRequest
    {
        public Guid Id { get; set; }
        public virtual Policeman Requester { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Topic { get; set; }
    }
}
