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
        public virtual Policeman RootPoliceman { get; set; }

        public static PoliceSection Create(Guid stationId, string name)
        {
            var newStation = new PoliceSection()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Address = new Address(),
                RootPoliceman = new Policeman(),
                Policemen = new List<Policeman>()

            };
            return newStation;
        }

    }
}
