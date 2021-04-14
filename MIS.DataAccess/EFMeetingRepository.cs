using MIS.DataAccess.Abstractions;
using MSI.DataAccess;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.DataAccess
{
    public class EFMeetingRepository : EFRepository<Meeting>, IMeetingRepository
    {
        private readonly PoliceContext context;

        public EFMeetingRepository(PoliceContext context) : base(context)
        {
            this.context = context;
        }
    }
}
