using System;
using System.Collections.Generic;

namespace API.Model
{
    public enum Status { Active , Closed};
    public class CriminalRecord
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public Policeman ModifiedBy { get; set; }
        public List<Policeman> Policemen { get; set; } = new List<Policeman>();
        public string Type { get; set; }
       
    }
}
