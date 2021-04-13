using MIS.DataAccess.Abstractions;
using MIS.Model;
using MSI.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.DataAccess
{
    public class EFMeetingRequestPolicemanRepository : EFRepository<MeetingRequestPoliceman> , IMeetingRequestPolicemanRepository
    {
        private readonly PoliceContext context;

        public EFMeetingRequestPolicemanRepository(PoliceContext context) : base(context)
        {
            this.context = context;
        }
    }
}
