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
    public class EFMeetingRequestPolicemanRepository : EFRepository<MeetingRequestPoliceman>, IMeetingRequestPolicemanRepository
    {
        private readonly PoliceContext context;

        public EFMeetingRequestPolicemanRepository(PoliceContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<Policeman> GetPolicemanForRequest(MeetingRequest request)
        {
            var requestPoliceman = GetAll();
            return requestPoliceman.Where(x => x.MeetingRequest == request).Select(x => x.Policeman);
        }

        public void RemoveAll(MeetingRequest request)
        {
            var requestPoliceman = GetAll();
            foreach (var it in requestPoliceman)
            {
                if(it.MeetingRequest == request)   
                    context.MeetingRequestPolicemen.Remove(it);
            }
            context.SaveChanges();
        }
    }
}
