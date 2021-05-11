using MIS.BusinessLogic.Filtering;
using MIS.DataAccess.Abstractions;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MIS.BusinessLogic
{
    public class PolicemanService
    {
        private readonly IPolicemanRepository policemanRepository;
        private readonly IPoliceSectionRepository policeSectionRepository;
        public PolicemanService(IPolicemanRepository policemanRepository, IPoliceSectionRepository policeSectionRepository)
        {
            this.policeSectionRepository = policeSectionRepository;
            this.policemanRepository = policemanRepository;
        }

        public Policeman GetByUserId(string id)
        {
            return policemanRepository.GetByUserId(Guid.Parse(id));
        }

        public IEnumerable<Policeman> GetAll()
        {
            return policemanRepository.GetAll();
        }

        public void Add(Policeman policeman)
        {
            policemanRepository.Add(policeman);

        }

        public IEnumerable<Policeman> SearchUsingEngine(SearchFilter searchFilter)
        {
            List<Policeman> criminalRecords = (List<Policeman>)policemanRepository.GetAll();
            var _searchEngine = new PolicemanSearchEngine(policemanRepository);
            return (_searchEngine.Search(searchFilter));
        }

        public void Update(Policeman policeman)
        {
            policemanRepository.Update(policeman);

        }

        public PoliceSection FindPolicemanSection(Policeman policeman)
        {
            return policeSectionRepository.GetAll().Where(x => x.Policemen.Contains(policeman)).FirstOrDefault();

        }

        public void Delete(Guid id)
        {
            FindPolicemanSection(Get(id));
            policemanRepository.Remove(id);
        }

        public void AddSubordinate(Guid policemanId, string email)
        {
            var policeman = policemanRepository.Get(policemanId);
            var subordinate = policemanRepository.GetByEmail(email);
            policeman.Subordinates.Add(subordinate);
            policemanRepository.Update(policeman);
        }

        public Policeman Get(Guid policemanId)
        {
            return policemanRepository.Get(policemanId);
        }

        public Policeman GetByEmail(string email)
        {
            return policemanRepository.GetByEmail(email);
        }
    }
}
