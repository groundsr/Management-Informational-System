using System;
using System.Collections.Generic;
using System.Text;

namespace MSI.Model
{
    public class PolicemanMeeting
    {
        public virtual Policeman Policeman { get; set; }
        public virtual Meeting Meeting { get; set; }
        public Guid Id { get; set; }
    }
}
