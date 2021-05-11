using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIS.BusinessLogic;
using MIS.DataAccess.Abstractions;
using Moq;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.ApplicationLogic.Tests
{
    [TestClass]
    public class PoliceServiceTests
    {
        PolicemanService policemanService;
        Mock<IPolicemanRepository> policemanRepository;
        Mock<IPoliceSectionRepository> policeStationRepository;
        public PoliceServiceTests()
        {
            policemanRepository = new Mock<IPolicemanRepository>();
            policeStationRepository = new Mock<IPoliceSectionRepository>();
            policemanService = new PolicemanService(policemanRepository.Object, policeStationRepository.Object);
        }

        [TestMethod]
        public void AddSubordinates_AddsTheSubordinateToThePoliceman()
        {
            Policeman policeman = new Policeman() { Id = Guid.NewGuid(), Email = "mihnea@mihnea.com" , Name ="Mihnea" };
            Policeman subordinate = new Policeman() { Id = Guid.NewGuid(), Email = "ionut@ionut.com", Name ="Ionut" };
            policemanRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(subordinate);
            policemanRepository.Setup(x => x.Get(It.IsAny<Guid>())).Returns(policeman);
            policemanRepository.Setup(x => x.Update(It.IsAny<Policeman>()));

            policemanService.AddSubordinate(policeman.Id, subordinate.Email);

            Assert.IsTrue(policeman.Subordinates.Contains(subordinate));
        }

    }
}
