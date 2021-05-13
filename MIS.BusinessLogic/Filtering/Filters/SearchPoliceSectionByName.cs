using MIS.DataAccess.Abstractions;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.BusinessLogic.Filtering.Filters
{
    public class SearchPoliceSectionByName:ISearchStrategy<PoliceSection>
    {
        private readonly IPoliceSectionRepository _policeSectionRepository;

        public SearchPoliceSectionByName(IPoliceSectionRepository policeSectionRepository)
        {
            _policeSectionRepository = policeSectionRepository;
        }

        public IEnumerable<PoliceSection> Search(string term)
        {
           return (IEnumerable<PoliceSection>)_policeSectionRepository.GetPoliceSectionByName(term);
        }
    }
}
