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
    public class EFCriminalRecordPolicemanRepository : EFRepository<CriminalRecordPoliceman>, IEFCriminalRecordPolicemanRepository
    {

        public EFCriminalRecordPolicemanRepository( PoliceContext policeContext):base(policeContext)
        {
        }

        public bool CheckIfPolicemanWasAdded(CriminalRecordPoliceman criminalRecordPoliceman)
        {

            var criminalRecordPolicemenCheck = _context.CriminalRecordPolicemen
                .Where(x => (x.CriminalRecord.Id == criminalRecordPoliceman.CriminalRecord.Id) &&
                (x.Policeman.Id == criminalRecordPoliceman.Policeman.Id))
                .FirstOrDefault();

            if(criminalRecordPolicemenCheck==null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public CriminalRecordPoliceman GetCriminalRecordPoliceman(CriminalRecord criminalRecord)
        {
            var criminalRecordPoliceman = _context.CriminalRecordPolicemen
                .Where(x => x.CriminalRecord.Id == criminalRecord.Id)
                .FirstOrDefault();

            return criminalRecordPoliceman;
        }

        public IEnumerable<CriminalRecordPoliceman> GetCriminalRecordPolicemenByRecordId(Guid id)
        {
            CriminalRecord criminalRecord = _context.CriminalRecords
                .Where(x => x.Id == id)
                .FirstOrDefault();

            List<CriminalRecordPoliceman> criminalRecordPolicemen =
                _context.CriminalRecordPolicemen
                .Where(x => x.CriminalRecord.Id == criminalRecord.Id)
                .ToList();

            return criminalRecordPolicemen;

        }

        public IEnumerable<CriminalRecordPoliceman> GetAllById(Guid id)
        {
            IEnumerable<CriminalRecordPoliceman> criminalRecordPolicemen =
                _context.CriminalRecordPolicemen
                .Where(x => x.Id == id)
                .ToList();

            return criminalRecordPolicemen;
        }

        public IEnumerable<CriminalRecordPoliceman> GetAllCriminalRecordsPolicemanForARecord(CriminalRecord criminalRecord)
        {
            IEnumerable<CriminalRecordPoliceman> criminalRecordPolicemen = _context.CriminalRecordPolicemen
                                .Where(x => x.CriminalRecord.Id == criminalRecord.Id)
                                .ToList();

            return criminalRecordPolicemen;

        }

        public IEnumerable<CriminalRecordPoliceman> GetAll(CriminalRecord criminalRecord)
        {
            IEnumerable<CriminalRecordPoliceman> criminalRecordList = (IEnumerable<CriminalRecordPoliceman>)
                _context.CriminalRecordPolicemen
                .Where(x => x.CriminalRecord.Id == criminalRecord.Id)
                .ToList();

            return criminalRecordList;
        }
    }
}
