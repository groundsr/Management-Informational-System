using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.Model
{
    public class MeetingRequestPoliceman
    {
        public Guid Id { get; set; }
        public virtual Policeman Policeman { get; set; }
        public virtual MeetingRequest MeetingRequest { get; set; }
    }
}
