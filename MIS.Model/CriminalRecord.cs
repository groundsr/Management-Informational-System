using System;
using System.Collections.Generic;

namespace MSI.Model
{
    public enum Status { Active , Closed};
    public class CriminalRecord
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public virtual Policeman ModifiedBy { get; set; }
        public string Type { get; set; }
       
    }
}
