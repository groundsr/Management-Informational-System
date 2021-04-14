using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.Model
{
    public class MeetingPoliceman
    {
        public Guid Id { get; set; }
        public virtual Policeman Policeman { get; set; }
        public virtual Meeting Meeting { get; set; }
    }
}
