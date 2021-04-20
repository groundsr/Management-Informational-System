using MIS.Model;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.DataAccess.Abstractions
{
    public interface IEFCriminalRecordPolicemanRepository:IRepository<CriminalRecordPoliceman>
    {
        CriminalRecordPoliceman GetCriminalRecordPoliceman(CriminalRecord criminalRecord);
        IEnumerable<CriminalRecordPoliceman> GetAll(CriminalRecord criminalRecord);
        bool CheckIfPolicemanWasAdded(CriminalRecordPoliceman criminalRecordPoliceman);
        IEnumerable<CriminalRecordPoliceman> GetAllById(Guid id);
        IEnumerable<CriminalRecordPoliceman> GetCriminalRecordPolicemenByRecordId(Guid id);

    }
}
