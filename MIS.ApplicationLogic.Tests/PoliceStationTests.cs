using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIS.BusinessLogic;
using MIS.DataAccess.Abstractions;
using Moq;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Tests
{
    [TestClass]
    public class PoliceStationTests
    {
        
        private Mock<IPolicemanRepository> _policemanRepository;
        private PoliceSectionService policeSectionService;
        private Mock<IPoliceSectionRepository> _policeSectionRepository;

        public PoliceStationTests()
        {
            _policemanRepository = new Mock<IPolicemanRepository>();
            _policeSectionRepository = new Mock<IPoliceSectionRepository>();
            policeSectionService = new PoliceSectionService(_policeSectionRepository.Object, _policemanRepository.Object);
        }


       
        [TestMethod]
        public void DeletePoliceSection()
        {
            PoliceSection policeSection = new PoliceSection { Id = Guid.NewGuid(), Name = "Sectia 1", };
            Guid myGuid = policeSection.Id;

            _policeSectionRepository.Setup(x => x.Remove(It.IsAny<Guid>())).Callback<Guid>((myGuid)
                => policeSectionService.Delete(myGuid));
        }

        [TestMethod]
        public void AddPolicemenToStation()
        {
            Policeman policeman = new Policeman { Id = Guid.NewGuid(), Name = "Silviu", Email = "silviu@silviu.com" };
            PoliceSection policeSection = new PoliceSection { Id = Guid.NewGuid(), Name = "Section 5" };

            _policemanRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(policeman);
            _policeSectionRepository.Setup(x => x.Get(It.IsAny<Guid>())).Returns(policeSection);
            _policemanRepository.Setup(x => x.Update(It.IsAny<Policeman>()));

            policeSectionService.AddPoliceToSection(policeSection, policeman.Email);

            Assert.IsTrue(policeSection.Policemen.Contains(policeman));

        }
        [TestMethod]
        public void PolicemanBelongToTheSameStation()
        {
            Policeman policeman = new Policeman { Id = Guid.NewGuid(), Name = "Silviu", Email = "silviu@silviu.com" };
            Policeman subordinate = new Policeman { Id = Guid.NewGuid(), Name = "Robert", Email = "robert@robert.com" };
            PoliceSection policeSection = new PoliceSection { Id = Guid.NewGuid(), Name = "Section 12" };

            _policemanRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(policeman);
            _policemanRepository.Setup(x => x.Get(It.IsAny<Guid>())).Returns(subordinate);
            _policemanRepository.Setup(x => x.Update(It.IsAny<Policeman>()));
            _policeSectionRepository.Setup(x => x.Get(It.IsAny<Guid>())).Returns(policeSection);
            _policeSectionRepository.Setup(x => x.Update(It.IsAny<PoliceSection>()));

            policeSectionService.AddPoliceToSection(policeSection, policeman.Email);
            policeSectionService.AddPoliceToSection(policeSection, subordinate.Email);

            

            Assert.IsTrue(policeSectionService.BelongToTheSameStation(policeman,subordinate,policeSection.Id));
            
        }

        
    }
}
