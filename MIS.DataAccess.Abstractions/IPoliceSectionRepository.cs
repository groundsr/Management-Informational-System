using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.DataAccess.Abstractions
{
    public interface IPoliceSectionRepository : IRepository<PoliceSection>
    {
        int GetPoliceSectionNumber(string policeSectionName);

        IEnumerable<PoliceSection> GetAll();
        
        PoliceSection GetPoliceSectionByName(string name);
    }
}
