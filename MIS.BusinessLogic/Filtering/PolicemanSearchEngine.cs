using MIS.BusinessLogic.Filtering.Filters;
using MIS.DataAccess.Abstractions;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.BusinessLogic.Filtering
{
    public class PolicemanSearchEngine
    {
        private readonly IDictionary<string, ISearchStrategy<Policeman>> policemanSearchStrategy = new Dictionary<string, ISearchStrategy<Policeman>>();
        private readonly IPolicemanRepository _policemanRepository;

        public PolicemanSearchEngine(IPolicemanRepository policemanRepository)
        {
            _policemanRepository = policemanRepository;
            policemanSearchStrategy["searchPoliceman"] = new SearchPolicemanByName(_policemanRepository);
        }

        public IEnumerable<Policeman> Search(SearchFilter searchFilter, Guid id = new Guid())
        {
            try
            {
                if (policemanSearchStrategy.ContainsKey(searchFilter.Name))
                {
                    {
                        var strategy = policemanSearchStrategy[searchFilter.Name];
                        return ((IEnumerable<Policeman>)strategy.Search(searchFilter.Term));
                    }
                }
            }
            catch (ArgumentNullException)
            {

            }
            return (IEnumerable<Policeman>)_policemanRepository.GetAll();
        }
    }
}

