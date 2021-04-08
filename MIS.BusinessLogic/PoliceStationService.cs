using MIS.DataAccess.Abstractions;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.BusinessLogic
{
    public class PoliceStationService
    {
        private readonly IPoliceStationRepository policeStationRepository;

        public PoliceStationService(IPoliceStationRepository policeStationRepository)
        {
            this.policeStationRepository = policeStationRepository;
        }

        public IEnumerable<PoliceSection> GetAll()
        {
            return policeStationRepository.GetAll();
        }


    }
}
