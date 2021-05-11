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
    public class EFCriminalRecordRepository : EFRepository<CriminalRecord>, ICriminalRecordRepository
    {

        public EFCriminalRecordRepository(PoliceContext policeContext) : base(policeContext)
        {

        }


        public IEnumerable<CriminalRecord> GetCriminalRecordBySection(Guid policeSectionId)
        {
            PoliceSection policeSection = _context.PoliceSections.Find(policeSectionId);
            List<Policeman> policemen = policeSection.Policemen;
            List<CriminalRecordPoliceman> criminalRecordPolicemen = new List<CriminalRecordPoliceman>();

            List<CriminalRecord> criminalRecords = new List<CriminalRecord>();
            foreach (var item in policemen)
            {
                List<CriminalRecordPoliceman> criminalRecordPolicemenTemp =
                (List<CriminalRecordPoliceman>)_context.CriminalRecordPolicemen
                .Where(x => x.Policeman.Id == item.Id)
                .ToList();



                foreach (var iterator in criminalRecordPolicemenTemp)
                {
                    criminalRecordPolicemen.Add(iterator);
                    criminalRecords.Add(iterator.CriminalRecord);
                }
            }

            return criminalRecords;
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

        public void EnableStatus(Guid criminalRecordId)
        {
            CriminalRecord record = _context.CriminalRecords
                    .Where(x => x.Id == criminalRecordId)
                    .FirstOrDefault();

            record.Status = Status.Active;
            _context.CriminalRecords.Update(record);
            _context.SaveChanges();
        }

        public void AddDocument(Document document, Guid criminalRecordId)
        {
            CriminalRecord criminalRecord = _context.CriminalRecords
                                           .Where(x => x.Id == criminalRecordId)
                                           .FirstOrDefault();

            criminalRecord.Documents.Add(document);
            _context.SaveChanges();
        }

        public bool CheckIfRecordExists(CriminalRecord criminalRecord)
        {
            if (!_context.CriminalRecords.Any(x => x.Name == criminalRecord.Name))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public IEnumerable<CriminalRecord> GetCriminalRecordByStatus(string boolFlag)
        {
            List<CriminalRecord> criminalRecords;

            if (boolFlag == "true")
            {
                criminalRecords = _context.CriminalRecords
                                .Where(x => x.Status == Status.Active)
                                .ToList();
            }
            else
            {
                criminalRecords = _context.CriminalRecords
                                    .Where(x => x.Status == Status.Closed)
                                    .ToList();
            }
            return criminalRecords;
        }


        public int GetStatus(CriminalRecord criminalRecord)
        {
            CriminalRecord criminalRecordToChange = _context.CriminalRecords
                    .Where(x => x.Id == criminalRecord.Id)
                    .FirstOrDefault();

            if (criminalRecordToChange.Status == Status.Active)
            {
                return 1;
            }
            else if (criminalRecordToChange.Status == Status.Closed)
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

        public IEnumerable<CriminalRecord> GetCriminalRecordsByName(IEnumerable<CriminalRecord> criminalRecords, string name)
        {
            List<CriminalRecord> filteredList = new List<CriminalRecord>();
            name = name.ToLower();

            foreach (var item in criminalRecords)
            {
                var itemName = item.Name.ToLower();
                if (itemName.Contains(name))
                {
                    filteredList.Add(item);
                }
            }

            return filteredList;
        }

        public IEnumerable<CriminalRecord> GetCriminalRecordsByNameBySection(Guid id, string name)
        {
            IEnumerable<CriminalRecord> allCriminalRecords = _context.CriminalRecords.ToList();
            IEnumerable<CriminalRecord> filteredCriminalRecords = this.GetCriminalRecordsByName(allCriminalRecords, name);
            return filteredCriminalRecords;
        }
    }
}
