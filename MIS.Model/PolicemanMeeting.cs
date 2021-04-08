using System;
using System.Collections.Generic;
using System.Text;

namespace API.Model
{
    public class PolicemanMeeting
    {
        public Policeman Policeman { get; set; }
        public Meeting Meeting { get; set; }
        public Guid Id { get; set; }
    }
}
