using System;
using System.Collections.Generic;
using System.Text;

namespace MSI.Model
{
    public class MeetingRequest
    {
        public Guid Id { get; set; }
        public virtual Policeman Requester { get; set; }
        public DateTime ScheduledOn { get; set; }
        public string Topic { get; set; }
        public virtual List<Policeman> Policemen { get; set; } = new List<Policeman>();
    }
}
