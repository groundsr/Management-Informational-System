using System;
using System.Collections.Generic;
using System.Text;

namespace MSI.Model
{
    public class Meeting
    {
        public Guid Id { get; set; }
        public List<Policeman> Policemen { get; set; } = new List<Policeman>();
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Topic { get; set; }
    }
}
