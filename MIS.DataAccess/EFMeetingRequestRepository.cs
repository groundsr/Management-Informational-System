using MIS.DataAccess.Abstractions;
using MSI.DataAccess;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.DataAccess
{
    public class EFMeetingRequestRepository : EFRepository<MeetingRequest>, IMeetingRequestRepository
    {
        private readonly PoliceContext context;

        public EFMeetingRequestRepository(PoliceContext context) : base(context)
        {
            this.context = context;
        }
    }
}
