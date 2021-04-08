using System;
using System.Collections.Generic;
using System.Text;

namespace API.Model
{
    public class PoliceSection
    {
        public Guid Id { get; set; }
        public List<Policeman> Policemen { get; set; } = new List<Policeman>();
        public string Name { get; set; }
        public Address Address { get; set; }

    }
}
