using MIS.DataAccess.Abstractions;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.BusinessLogic
{
    public class PoliceSectionService
    {
        private readonly IPoliceSectionRepository policeStationRepository;
        private readonly IPolicemanRepository policemanRepository;
        private readonly CriminalRecordService _criminalRecordService;

        public PoliceSectionService(IPoliceSectionRepository policeStationRepository, IPolicemanRepository policemanRepository, CriminalRecordService criminalRecordService)
        {
            this.policeStationRepository = policeStationRepository;
            this.policemanRepository = policemanRepository;
            _criminalRecordService = criminalRecordService;
        }

        public void AddPoliceToSection(PoliceSection policeSection, string email)
        {
            var policeman = policemanRepository.GetByEmail(email);
            policeSection.Policemen.Add(policeman);
            policeStationRepository.Update(policeSection);
        }

        public IEnumerable<PoliceSection> GetAll()
        {
            return policeStationRepository.GetAll();
        }

        public void Add(PoliceSection policeSection)
        {
            policeStationRepository.Add(policeSection);
        }

        public void Update(PoliceSection policeSection)
        {

            policeStationRepository.Update(policeSection);
        }

        public IEnumerable<CriminalRecord> GetCriminalRecordsByName(string name)
        {
            return (_criminalRecordService.GetCriminalRecordsByName(name));
        }

        public IEnumerable<CriminalRecord> GetCriminalRecordsByNameBySection(Guid id, string name)
        {
            IEnumerable<CriminalRecord> allCriminalRecords = _criminalRecordService.GetCriminalRecordBySection(id);
            IEnumerable<CriminalRecord> filteredCriminalRecords = _criminalRecordService.GetCriminalRecordsByNameBySection(id, name);
            return filteredCriminalRecords;

        }


        public PoliceSection Get(Guid id)
        {
            return policeStationRepository.Get(id);
        }

        public void Delete(Guid id)
        {
            var policeStation = policeStationRepository.Get(id);
            policeStation.Policemen.Clear();
            Update(policeStation);

            policeStationRepository.Remove(id);
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



    }
}
