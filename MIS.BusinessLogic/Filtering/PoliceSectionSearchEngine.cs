using MIS.BusinessLogic.Filtering.Filters;
using MIS.DataAccess.Abstractions;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.BusinessLogic.Filtering
{
    public class PoliceSectionSearchEngine
    {
        private readonly IDictionary<string, ISearchStrategy<PoliceSection>> policeSectionSearchStrategy = new Dictionary<string, ISearchStrategy<PoliceSection>>();
        private readonly IPoliceSectionRepository _policeSectionRepository;

        public PoliceSectionSearchEngine(IEnumerable<PoliceSection> policeSections, IPoliceSectionRepository policeSectionRepository)
        {
            _policeSectionRepository = policeSectionRepository;
            policeSectionSearchStrategy["searchByName"] = new SearchPoliceSectionByName(policeSectionRepository, policeSections);
        }

        public IEnumerable<PoliceSection> Search(SearchFilter searchFilter)
        {
            try
            {
                if (policeSectionSearchStrategy.ContainsKey(searchFilter.Name))
                {
                    var strategy = policeSectionSearchStrategy[searchFilter.Name];
                    return (IEnumerable<PoliceSection>)strategy.Search(searchFilter.Term);
                }
            }
            catch (ArgumentNullException)
            {

            }
            return ((IEnumerable<PoliceSection>)_policeSectionRepository.GetAll());
        }
    }
}
