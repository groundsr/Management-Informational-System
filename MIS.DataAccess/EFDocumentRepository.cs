

using MIS.DataAccess.Abstractions;
using MIS.Model;
using MSI.DataAccess;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MIS.DataAccess
{
    public class EFDocumentRepository:EFRepository<Document>,IDocumentRepository
    {
        private readonly PoliceContext _context;

        public EFDocumentRepository(PoliceContext context):base(context)
        {

        }

        public bool CheckIfDocumentWasAdded(Document document)
        {
            return (_context.Documents.Any(x => x.Name == document.Name));
        }

        public IEnumerable<Document> GetDocuments(Guid criminalRecordId)
        {
            CriminalRecord criminalRecord = _context.CriminalRecords
                .Where(x => x.Id == criminalRecordId)
                .FirstOrDefault();

            List<Document> documents = criminalRecord.Documents;
            return documents;

            
        }
    }
}
