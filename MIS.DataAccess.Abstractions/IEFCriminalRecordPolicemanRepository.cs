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
        void AddPolicemanToCriminalRecord(Policeman policeman, CriminalRecord criminalRecord);
        IEnumerable<CriminalRecordPoliceman> GetAll(CriminalRecord criminalRecord);
        bool CheckIfPolicemanWasAdded(CriminalRecordPoliceman criminalRecordPoliceman);

    }
}
