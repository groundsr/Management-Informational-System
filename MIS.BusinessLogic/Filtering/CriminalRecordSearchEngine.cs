using MIS.BusinessLogic.Filtering.Filters;
using MIS.DataAccess.Abstractions;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.BusinessLogic.Filtering
{
    public class CriminalRecordSearchEngine
    {
        private readonly IDictionary<string,ISearchStrategy<CriminalRecord>> criminalRecordSearchStrategy=new Dictionary<string,ISearchStrategy<CriminalRecord>>();
        
        private readonly ICriminalRecordRepository _criminalRecordRepository;
        private readonly ICriminalRecordPolicemanRepository _criminalRecordPolicemanRepository;
        public CriminalRecordSearchEngine(IEnumerable<CriminalRecord> criminalRecords, ICriminalRecordRepository criminalRecordRepository, ICriminalRecordPolicemanRepository criminalRecordPolicemanRepository)
        {
            this._criminalRecordRepository = criminalRecordRepository;
            _criminalRecordPolicemanRepository = criminalRecordPolicemanRepository;
            criminalRecordSearchStrategy["recordName"] = new SearchCriminalRecordByName(_criminalRecordRepository, criminalRecords);
            criminalRecordSearchStrategy["policemanName"] = new SearchCriminalRecordByPolicemanName(_criminalRecordPolicemanRepository, criminalRecords);
            criminalRecordSearchStrategy["criminalRecordsUsingId"] = new SearchCriminalRecordBySection(_criminalRecordRepository, criminalRecords);
        }

        public IEnumerable<CriminalRecord> Search(SearchFilter searchFilter,Guid id=new Guid())
        
        {
            try
            {
                if (criminalRecordSearchStrategy.ContainsKey(searchFilter.Name))
                {
                    if(searchFilter.Name.Contains("Id"))
                    {
                        ISearchStrategyUsingId<CriminalRecord> strategy = (ISearchStrategyUsingId<CriminalRecord>)criminalRecordSearchStrategy[searchFilter.Name];
                        return (strategy.SearchUsingId(searchFilter.Term,id));
                    }
                    else
                    {
                        var strategy = criminalRecordSearchStrategy[searchFilter.Name];
                        return (strategy.Search(searchFilter.Term));
                    }
                }
            }
            catch(ArgumentNullException)
            {

            }
            return (IReadOnlyCollection<CriminalRecord>)_criminalRecordRepository.GetAll();
        }
    }
}
