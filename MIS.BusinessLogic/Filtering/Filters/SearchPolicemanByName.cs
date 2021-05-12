using MIS.DataAccess.Abstractions;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.BusinessLogic.Filtering.Filters
{
    public class SearchPolicemanByName : ISearchStrategy<Policeman>
    {
        private readonly IPolicemanRepository _policemanRepository;

        public SearchPolicemanByName(IPolicemanRepository policemanRepository)
        {
            _policemanRepository = policemanRepository;
        }

        public IEnumerable<Policeman> Search(string term)
        {
            return (_policemanRepository.GetPolicemanByName(term));
        }
    }
}
