using MIS.Model;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.DataAccess.Abstractions
{
    public interface IMeetingRequestPolicemanRepository : IRepository<MeetingRequestPoliceman>
    {
        void RemoveAll(MeetingRequest request);
        IEnumerable<Policeman> GetPolicemanForRequest(MeetingRequest request);
    }
}
