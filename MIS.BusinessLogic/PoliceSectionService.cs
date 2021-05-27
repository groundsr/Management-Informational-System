using MIS.BusinessLogic.Filtering;
using MIS.DataAccess.Abstractions;
using MIS.Model;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.BusinessLogic
{
    public class PoliceSectionService
    {
        private readonly IPoliceSectionRepository _policeSectionRepository;
        private readonly IPolicemanRepository policemanRepository;
        private readonly ICriminalRecordRepository _criminalRecordRepository;
        private readonly ICriminalRecordPolicemanRepository _criminalRecordPoliceman;

        public PoliceSectionService(IPoliceSectionRepository policeStationRepository, IPolicemanRepository policemanRepository, ICriminalRecordRepository criminalRecordService, ICriminalRecordPolicemanRepository criminalRecordPolicemanRepository)
        {
            this._policeSectionRepository = policeStationRepository;
            this.policemanRepository = policemanRepository;
            _criminalRecordRepository = criminalRecordService;
            _criminalRecordPoliceman = criminalRecordPolicemanRepository;
        }
        public PoliceSectionService(IPoliceSectionRepository policeSectionRepository,IPolicemanRepository policemanRepository)
        {
            this._policeSectionRepository = policeSectionRepository;
            this.policemanRepository = policemanRepository;
        }
        public void AddPoliceToSection(PoliceSection policeSection, string email)
        {
            var policeman = policemanRepository.GetByEmail(email);
            policeSection.Policemen.Add(policeman);
            if(policeSection.RootPoliceman==null)
            {
                policeSection.RootPoliceman = policeman;
            }
            // policeStationRepository.Update(policeSection);
            _policeSectionRepository.Update(policeSection);
        }

        public IEnumerable<PoliceSection> GetAll()
        {
            return _policeSectionRepository.GetAll();
        }

        public void Add(PoliceSection policeSection)
        {
            
            // policeStationRepository.Add(policeSection);
            
            _policeSectionRepository.Add(policeSection);
        }

        public IEnumerable<CriminalRecord> GetCriminalRecordsBySection(Guid id)
        {
            return _criminalRecordRepository.GetCriminalRecordBySection(id);
        }

        public IEnumerable<CriminalRecord> GetCriminalRecordsByNameBySection(Guid id, SearchFilter searchedRecord)
        {
            var policemen = Get(id).Policemen;
            var criminalRecords = _criminalRecordRepository.GetAll();
            CriminalRecordSearchEngine criminalRecordSearchEngine = new CriminalRecordSearchEngine(criminalRecords, _criminalRecordRepository, _criminalRecordPoliceman);
           return (criminalRecordSearchEngine.Search(searchedRecord,id));
        }

        public IEnumerable<CriminalRecord> GetCriminalRecordsByName(string name)
        {
            return (_criminalRecordRepository.GetCriminalRecordsByName(name));
        }


        public PoliceSection Get(Guid id)
        {
            return _policeSectionRepository.Get(id);
        }

        public Guid GetPoliceSectionId(PoliceSection policeSection)
        {
            return policeSection.Id;
        }

        public void Delete(Guid id)
        {
            var policeStation = _policeSectionRepository.Get(id);
            policeStation.Policemen.Clear();
           _policeSectionRepository.Update(policeStation);

            _policeSectionRepository.Remove(id);
        }

        public Dictionary<Policeman, List<Policeman>> PolicemenHierarchy(Guid sectionId)
        {
            var section = Get(sectionId);
            Dictionary<Policeman, List<Policeman>> hierarchy = new Dictionary<Policeman, List<Policeman>>();
            Queue<Policeman> bfsQueue = new Queue<Policeman>();
            bfsQueue.Enqueue(section.RootPoliceman);
            while (bfsQueue.Count > 0)
            {
                var front = bfsQueue.Dequeue();
                foreach (var it in front.Subordinates)
                {
                    bfsQueue.Enqueue(it);
                }
                if (front.Subordinates.Count > 0)
                {
                    hierarchy.Add(front, front.Subordinates);
                }

            }
            return hierarchy;
        }

        public List<Address> GetSectionsAddress()
        {
            return _policeSectionRepository.GetAll().Select(x => x.Address).ToList();
        }
        public bool BelongToTheSameStation(Policeman policeman , Policeman subordinate , Guid sectionId)
        {
            var section = Get(sectionId);
            int found = 0;
            foreach(var it in section.Policemen)
            {
                if(it == policeman || it == subordinate)
                {
                    found++;
                }
            }
            return found == 2;
        }
        public IEnumerable<PoliceSection> GetPoliceSectionsByName(SearchFilter searchFilter)
        {
            IEnumerable<PoliceSection> policeSections = (IEnumerable<PoliceSection>)_policeSectionRepository.GetAll();
            PoliceSectionSearchEngine searchEngine = new PoliceSectionSearchEngine(policeSections, _policeSectionRepository);
            return (IEnumerable<PoliceSection>)searchEngine.Search(searchFilter);
        }

        public void Update(PoliceSection policeSection)
        {
            _policeSectionRepository.Update(policeSection);
            _policeSectionRepository.Save();
        }



    }
}
