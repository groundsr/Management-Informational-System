using MIS.BusinessLogic.Filtering.Filters;
using MIS.DataAccess.Abstractions;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.BusinessLogic.Filtering
{
    public class SearchEngine
    {
        private readonly IDictionary<string,ISearchStrategy<CriminalRecord>> searchStrategies=new Dictionary<string,ISearchStrategy<CriminalRecord>>();
        private readonly ICriminalRecordRepository _criminalRecordRepository;
        private readonly ICriminalRecordPolicemanRepository _criminalRecordPolicemanRepository;

        public SearchEngine(ICollection<CriminalRecord> criminalRecords, ICriminalRecordRepository criminalRecordRepository, ICriminalRecordPolicemanRepository criminalRecordPolicemanRepository)
        {
            _criminalRecordRepository = criminalRecordRepository;
            _criminalRecordPolicemanRepository = criminalRecordPolicemanRepository;
            searchStrategies["recordName"] = new SearchCriminalRecordByName(criminalRecordRepository, criminalRecords);
            searchStrategies["policemanName"] = new SearchCriminalRecordByPolicemanName(_criminalRecordPolicemanRepository, criminalRecords);
        }


        public IEnumerable<CriminalRecord> Search(SearchFilter searchFilter)
        {
            try
            {
                if (searchStrategies.ContainsKey(searchFilter.Name))
                {
                    var strategy = searchStrategies[searchFilter.Name];
                    return strategy.Search(searchFilter.Term);
                }
            }
            catch(ArgumentNullException)
            {

            }
            return (IReadOnlyCollection<CriminalRecord>)_criminalRecordRepository.GetAll();
        }
    }
}
