using MIS.DataAccess.Abstractions;
using MSI.DataAccess;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.DataAccess
{
    public class EFPoliceSectionRepository : EFRepository<PoliceSection>, IPoliceSectionRepository
    {
        private readonly PoliceContext context;

        public EFPoliceSectionRepository(PoliceContext context) : base (context)
        {
            this.context = context;
        }

        public IEnumerable<PoliceSection> GetAll()
        {
            IEnumerable<PoliceSection> policeSections = _context.PoliceSections.ToList().AsEnumerable();
            return (policeSections);
        }


        public IEnumerable<PoliceSection> GetPoliceSectionByName(string name)
        {
            return (_context.PoliceSections
                 .Where(x => x.Name.Contains(name))
                 .ToList()
                 .AsEnumerable());
        }




    }
}
