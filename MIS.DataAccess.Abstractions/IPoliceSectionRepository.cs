using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.DataAccess.Abstractions
{
    public interface IPoliceSectionRepository : IRepository<PoliceSection>
    {

        IEnumerable<PoliceSection> GetAll();

        IEnumerable<PoliceSection> GetPoliceSectionByName(string name);
    }
}
