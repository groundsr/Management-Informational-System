using MIS.DataAccess.Abstractions;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.BusinessLogic.Filtering.Filters
{
    public class SearchCriminalRecordByStatus:ISearchStrategy<CriminalRecord>
    {
        private readonly ICriminalRecordRepository _criminalRecordRepository;

        public SearchCriminalRecordByStatus(ICriminalRecordRepository criminalRecordRepository)
        {
            _criminalRecordRepository = criminalRecordRepository;
        }

        public IEnumerable<CriminalRecord> Search(string boolFlag)
        {
            return _criminalRecordRepository.GetCriminalRecordByStatus(boolFlag);
        }
    }
}
