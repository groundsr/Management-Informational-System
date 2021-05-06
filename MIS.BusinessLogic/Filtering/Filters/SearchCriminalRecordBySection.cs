using MIS.DataAccess.Abstractions;
using MSI.Model;
using System;
using System.Collections.Generic;

namespace MIS.BusinessLogic.Filtering.Filters
{
    public class SearchCriminalRecordBySection:ISearchStrategyUsingId<CriminalRecord>
    {

        private readonly ICriminalRecordRepository _criminalRecordRepository;
        private readonly IEnumerable<CriminalRecord> _criminalRecords;

        public SearchCriminalRecordBySection(ICriminalRecordRepository criminalRecordRepository, IEnumerable<CriminalRecord> criminalRecords)
        {
            _criminalRecordRepository = criminalRecordRepository;
            _criminalRecords = criminalRecords;
        }

        public IEnumerable<CriminalRecord> Search(string term)
        {
            return (_criminalRecordRepository.GetAll());
        }

        public IEnumerable<CriminalRecord> SearchUsingId(string term, Guid id)
        {
           return (_criminalRecordRepository.GetCriminalRecordsByNameBySection(id, term));
        }
    }
}