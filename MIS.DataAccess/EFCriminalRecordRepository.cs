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
    public class EFCriminalRecordRepository:EFRepository<CriminalRecord>,ICriminalRecordRepository
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
         
        public void AddDocument(Document document,Guid criminalRecordId)
        {
            CriminalRecord criminalRecord = _context.CriminalRecords
                                           .Where(x => x.Id == criminalRecordId)
                                           .FirstOrDefault();

            criminalRecord.Documents.Add(document);
            _context.SaveChanges();
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


        public int GetStatus(CriminalRecord criminalRecord)
        {
            CriminalRecord criminalRecordToChange = _context.CriminalRecords
                    .Where(x => x.Id == criminalRecord.Id)
                    .FirstOrDefault();

            if(criminalRecordToChange.Status==Status.Active)
            {
                return 1;
            }
            else if(criminalRecordToChange.Status==Status.Closed)
            {
                return 0;
            }

            return 1;
        }

        public IEnumerable<Document> GetDocuments(Guid criminalRecordId)
        {
            CriminalRecord criminalRecord = _context.CriminalRecords
                .Where(x => x.Id == criminalRecordId)
                .FirstOrDefault();

            List<Document> documents = criminalRecord.Documents;
            return documents;
        }

        public IEnumerable<CriminalRecord> GetCriminalRecordsByName(IEnumerable<CriminalRecord> criminalRecords,string name)
        {
            List<CriminalRecord> filteredList = new List<CriminalRecord>();
            name = name.ToLower();
            foreach(var item in criminalRecords)
            {
                var itemName = item.Name.ToLower();
                if(itemName.Contains(name))
                {
                    filteredList.Add(item);
                }
            }

            return filteredList;
        }


    }
}
