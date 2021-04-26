using MIS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.DataAccess.Abstractions
{
    public interface IDocumentRepository:IRepository<Document>
    {
        bool CheckIfDocumentWasAdded(Document document);

        IEnumerable<Document> GetDocuments(Guid criminalRecordId);
    }
}
