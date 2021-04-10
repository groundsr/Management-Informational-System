using MIS.DataAccess.Abstractions;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.BusinessLogic
{
    public class PoliceSectionService
    {
        private readonly IPoliceSectionRepository policeStationRepository;

        public PoliceSectionService(IPoliceSectionRepository policeStationRepository)
        {
            this.policeStationRepository = policeStationRepository;
        }

        public IEnumerable<PoliceSection> GetAll()
        {
            return policeStationRepository.GetAll();
        }

        public void Add(PoliceSection policeSection)
        {
            policeStationRepository.Add(policeSection);
        }


    }
}
