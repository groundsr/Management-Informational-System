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
        private readonly IEnumerable<PoliceSection> _policeSections;

        public SearchPoliceSectionByName(IPoliceSectionRepository policeSectionRepository, IEnumerable<PoliceSection> policeSections)
        {
            _policeSectionRepository = policeSectionRepository;
            _policeSections = policeSections;
        }

        public IEnumerable<PoliceSection> Search(string term)
        {
           return (IEnumerable<PoliceSection>)_policeSectionRepository.GetPoliceSectionByName(term);
        }
    }
}
