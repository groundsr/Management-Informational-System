using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.Model
{
    public class CriminalRecordPoliceman
    {
        public Guid Id { get; set; }
        public virtual Policeman Policeman { get; set; }
        public virtual CriminalRecord CriminalRecord { get; set; }
        public DateTime DateWhenWasAdded {get;set;}
    }
}
