using MIS.Model;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.DataAccess.Abstractions
{
    public interface IMeetingPolicemanRepository : IRepository<MeetingPoliceman>
    {
        IEnumerable<MeetingPoliceman> GetAllForPoliceman(Policeman policeman);
    }
}
