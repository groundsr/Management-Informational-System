using MIS.Model;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.DataAccess.Abstractions
{
    public interface ICriminalRecordRepository:IRepository<CriminalRecord>
    {
        bool CheckIfRecordExists(CriminalRecord criminalRecord);
        IEnumerable<CriminalRecord> GetCriminalRecordsByName(string name);
        int GetStatus(CriminalRecord criminalRecord);
        void AddDocument(Document document, Guid criminalRecordId);
        IEnumerable<Document> GetDocuments(Guid criminalRecordId);
        IEnumerable<CriminalRecord> GetCriminalRecordsByName(IEnumerable<CriminalRecord> criminalRecords, string name);
        IEnumerable<CriminalRecord> GetCriminalRecordByStatus(string boolFlag);
        IEnumerable<CriminalRecord> GetCriminalRecordBySection(Guid policeSectionId);
        IEnumerable<CriminalRecord> GetCriminalRecordsByNameBySection(Guid id, string name);
        void EnableStatus(Guid criminalRecordId);


    }
}
