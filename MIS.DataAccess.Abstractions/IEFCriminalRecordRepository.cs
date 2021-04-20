using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.DataAccess.Abstractions
{
    public interface IEFCriminalRecordRepository:IRepository<CriminalRecord>
    {
        bool CheckIfRecordExists(CriminalRecord criminalRecord);
        IEnumerable<CriminalRecord> GetCriminalRecordsByName(string name);

        int GetStatus(CriminalRecord criminalRecord);


    }
}
