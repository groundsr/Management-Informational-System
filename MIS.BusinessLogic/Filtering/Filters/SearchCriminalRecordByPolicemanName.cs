using MIS.DataAccess.Abstractions;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.BusinessLogic.Filtering.Filters
{
    public class SearchCriminalRecordByPolicemanName:ISearchStrategy<CriminalRecord>
    {
        private readonly ICriminalRecordPolicemanRepository _criminalRecordPolicemanRepository;
        private readonly IEnumerable<CriminalRecord> _criminalRecords;

        public SearchCriminalRecordByPolicemanName(ICriminalRecordPolicemanRepository criminalRecordPolicemanRepository, IEnumerable<CriminalRecord> criminalRecords)
        {
            _criminalRecordPolicemanRepository = criminalRecordPolicemanRepository;
            _criminalRecords = criminalRecords;
        }

        public IEnumerable<CriminalRecord> Search(string policemanName)
        {
            return (_criminalRecordPolicemanRepository.GetCriminalRecordsByPolicemanName(policemanName));
        }
    }
}
