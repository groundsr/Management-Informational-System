using System;
using System.Collections.Generic;
using System.Text;

namespace MSI.Model
{
    public class MeetingRequest
    {
        public Guid Id { get; set; }
        public Policeman Requester { get; set; }
        public DateTime ScheduledOn { get; set; }
        public string Topic { get; set; }
        public List<Policeman> Policemen { get; set; } = new List<Policeman>();
    }
}
