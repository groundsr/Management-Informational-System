using MIS.DataAccess.Abstractions;
using MIS.Model;
using MSI.DataAccess;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.DataAccess
{
    public class EFMeetingPolicemanRepository : EFRepository<MeetingPoliceman>, IMeetingPolicemanRepository
    {
        private readonly PoliceContext context;

        public EFMeetingPolicemanRepository(PoliceContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<MeetingPoliceman> GetAllForPoliceman(Policeman policeman)
        {
            return context.MeetingPolicemen.Where(x => x.Policeman == policeman);
        }

    }
}
