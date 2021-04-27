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

        public void AddPoliceToSection(PoliceSection policeSection, string email)
        {
            var policeman = policemanRepository.GetByEmail(email);
            policeSection.Policemen.Add(policeman);
            _policeSectionRepository.Update(policeSection);
        }

        public IEnumerable<PoliceSection> GetAll()
        {
            return _policeSectionRepository.GetAll();
        }

        public void Add(PoliceSection policeSection)
        {
            _policeSectionRepository.Add(policeSection);
        }

        public void Update(PoliceSection policeSection)
        {

            _policeSectionRepository.Update(policeSection);
        }


        public IEnumerable<CriminalRecord> GetCriminalRecordsByName(string name)
        {
            return (_criminalRecordRepository.GetCriminalRecordsByName(name));
        }

        public IEnumerable<CriminalRecord> GetCriminalRecordsByNameBySection(Guid id, string name)
        {
            IEnumerable<CriminalRecord> allCriminalRecords = this.GetCriminalRecordBySection(id);
            IEnumerable<CriminalRecord> filteredCriminalRecords = _criminalRecordRepository.GetCriminalRecordsByName(allCriminalRecords, name);
            return filteredCriminalRecords;

        }


        public PoliceSection Get(Guid id)
        {
            return _policeSectionRepository.Get(id);
        }

        public void Delete(Guid id)
        {
            var policeStation = _policeSectionRepository.Get(id);
            policeStation.Policemen.Clear();
            Update(policeStation);

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

        public IEnumerable<CriminalRecord> GetCriminalRecordBySection(Guid policeSectionId)
        {
            PoliceSection policeSection = _policeSectionRepository.Get(policeSectionId);
            List<Policeman> policemen = policeSection.Policemen;
            List<CriminalRecordPoliceman> criminalRecordPolicemen = new List<CriminalRecordPoliceman>();

            List<CriminalRecord> criminalRecords = new List<CriminalRecord>();
            foreach (var item in policemen)
            {
                List<CriminalRecordPoliceman> criminalRecordPolicemenTemp =
                (List<CriminalRecordPoliceman>)_criminalRecordPoliceman.GetAllCriminalRecordPoliceman(item);

                foreach (var iterator in criminalRecordPolicemenTemp)
                {
                    criminalRecordPolicemen.Add(iterator);
                    criminalRecords.Add(iterator.CriminalRecord);
                    break;
                }
            }

            return criminalRecords;


        }



    }
}
