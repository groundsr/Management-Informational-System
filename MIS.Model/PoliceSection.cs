using System;
using System.Collections.Generic;
using System.Text;

namespace MSI.Model
{
    public class PoliceSection
    {
        public Guid Id { get; set; }
        public virtual List<Policeman> Policemen { get; set; } = new List<Policeman>();
        public string Name { get; set; }
        public virtual Address Address { get; set; }

    }
}
