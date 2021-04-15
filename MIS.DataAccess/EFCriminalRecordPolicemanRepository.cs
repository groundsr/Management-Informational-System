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
    public class EFCriminalRecordPolicemanRepository:EFRepository<CriminalRecordPoliceman>,IEFCriminalRecordPolicemanRepository
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

        public void AddPolicemanToCriminalRecord(Policeman policeman,CriminalRecord criminalRecord)
        {
            var recordPoliceman= _context.CriminalRecordPolicemen
                .Where(x => x.CriminalRecord.Id == criminalRecord.Id)
                .FirstOrDefault();


            recordPoliceman.CriminalRecord = criminalRecord;


            if (recordPoliceman.Policeman == null)
            {
                recordPoliceman.Policeman = policeman;
                _context.Update(recordPoliceman);
            }
            else
            {
                var newRecordPoliceman = new CriminalRecordPoliceman();
                newRecordPoliceman.CriminalRecord = criminalRecord;
                newRecordPoliceman.Policeman = policeman;


                if (!(CheckIfPolicemanWasAdded(newRecordPoliceman)))
                {
                    _context.CriminalRecordPolicemen.Add(newRecordPoliceman);
                }

            }
            _context.SaveChanges();

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
