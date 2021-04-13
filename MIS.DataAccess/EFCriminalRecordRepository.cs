using MIS.DataAccess.Abstractions;
using MSI.DataAccess;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.DataAccess
{
    public class EFCriminalRecordRepository:EFRepository<CriminalRecord>,IEFCriminalRecordRepository
    {

        public EFCriminalRecordRepository(PoliceContext policeContext):base(policeContext)
        {

        }

        public IEnumerable<CriminalRecord> GetCriminalRecordsByName(string name)
        {

            if (name != null)
            {
                IEnumerable<CriminalRecord> criminalRecords = _context.CriminalRecords
                  .Where(x => x.Name.Contains(name));
                
                return criminalRecords;

            }

            return null;

        }
         

        public bool CheckIfRecordExists(CriminalRecord criminalRecord)
        {
            if(!_context.CriminalRecords.Any(x=>x.Name==criminalRecord.Name))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
