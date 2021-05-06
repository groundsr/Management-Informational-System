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

        public PoliceSectionService(IPoliceSectionRepository policeStationRepository, IPolicemanRepository policemanRepository)
        {
            this.policeStationRepository = policeStationRepository;
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

        public PoliceSection Get(Guid id)
        {
            return policeStationRepository.Get(id);
        }

        public Guid GetPoliceSectionId(PoliceSection policeSection)
        {
            return policeSection.Id;
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

        public List<Address> GetSectionsAddress()
        {
            return policeStationRepository.GetAll().Select(x => x.Address).ToList();
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


    }
}
