using MIS.DataAccess.Abstractions;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.BusinessLogic.Filtering.Filters
{
    public class SearchCriminalRecordByName : ISearchStrategy<CriminalRecord>
    {

        private readonly ICriminalRecordRepository _criminalRecordRepository;
        private readonly ICollection<CriminalRecord> _criminalRecords;

        public SearchCriminalRecordByName(ICriminalRecordRepository criminalRecordRepository, ICollection<CriminalRecord> criminalRecords)
        {
            _criminalRecordRepository = criminalRecordRepository;
            this._criminalRecords = criminalRecords;
        }

        public IEnumerable<CriminalRecord> Search(string term)
        {
           return(_criminalRecordRepository.GetCriminalRecordsByName(term));
        }


    }
}
