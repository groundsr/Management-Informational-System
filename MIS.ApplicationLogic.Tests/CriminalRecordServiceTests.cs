using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIS.BusinessLogic;
using MIS.DataAccess.Abstractions;
using MIS.DTOs.BusinessLogic;
using MIS.Model;
using Moq;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Tests
{
    [TestClass]
    public class CriminalRecordServiceTests
    {
        private Mock<ICriminalRecordRepository> _criminalRecordsRepository;
        private Mock<ICriminalRecordPolicemanRepository> _criminalRecordPolicemanRepository;
        private Mock<IPolicemanRepository> _policemanRepository;
        private Mock<IPoliceSectionRepository> _policeSectionRepository;
        private Mock<IDocumentRepository> _documentRepository;
        private CriminalRecordService _criminalRecordService;

        public CriminalRecordServiceTests()
        {
            _criminalRecordsRepository = new Mock<ICriminalRecordRepository>();
            _criminalRecordPolicemanRepository = new Mock<ICriminalRecordPolicemanRepository>();
            _policemanRepository = new Mock<IPolicemanRepository>();
            _policeSectionRepository = new Mock<IPoliceSectionRepository>();
            _documentRepository = new Mock<IDocumentRepository>();
            _criminalRecordService = new BusinessLogic.CriminalRecordService(_criminalRecordsRepository.Object
                ,_criminalRecordPolicemanRepository.Object,_policemanRepository.Object,null,
                _documentRepository.Object,_policeSectionRepository.Object);
        }

        [TestMethod]
        public void AddPolicemanToCriminalRecord_ThrowExceptionForNullPoliceman()
        {
            CriminalRecord criminalRecord=new CriminalRecord { Id=Guid.NewGuid(), Name="AnthonyMurder"};
            Policeman policeman = new Policeman { Id = Guid.NewGuid(), Name = "Bogdan Hurza", Email = "bogdan@bogdan.com" };
            
            CriminalRecordPoliceman criminalRecordPolicemanTest=new CriminalRecordPoliceman();
            List<CriminalRecordPoliceman> criminalRecordPolicemen = new List<CriminalRecordPoliceman>();

            _policemanRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(policeman);
            _policemanRepository.Setup(x => x.Get(It.IsAny<Guid>())).Returns(policeman);
            _policemanRepository.Setup(x => x.Update(It.IsAny<Policeman>()));

            _criminalRecordsRepository.Setup(x => x.Get(It.IsAny<Guid>()));
            _criminalRecordPolicemanRepository.Setup(x => x.Update(It.IsAny<CriminalRecordPoliceman>()));
            _criminalRecordPolicemanRepository.Setup(x => x.GetCriminalRecordPoliceman(It.IsAny<CriminalRecord>())).Returns(criminalRecordPolicemanTest);
            _criminalRecordPolicemanRepository.Setup(x => x.CheckIfPolicemanWasAdded(It.IsAny<CriminalRecordPoliceman>())).Returns(false);
            _criminalRecordPolicemanRepository.Setup(x => x.Add(It.IsAny<CriminalRecordPoliceman>()));
            _criminalRecordPolicemanRepository.Setup(x => x.GetAllCriminalRecordsPolicemanForARecord(It.IsAny<CriminalRecord>())).Returns(criminalRecordPolicemen);
            
            
            Assert.ThrowsException<Exception>(
                     () =>
                     {
                         _criminalRecordService.AddPolicemanToCriminalRecord(null, criminalRecord);
                     });
        }

        [TestMethod]
        public void CreateCriminalRecord_ThrowsExceptionForNameDescriptionType()
        {
            CriminalRecord criminalRecord = new CriminalRecord { Id = Guid.NewGuid()};
            
            criminalRecord.Name = "TestName";
            criminalRecord.Type = "Murder";
            criminalRecord.Description = "Minimum 20 chars";

            _criminalRecordsRepository.Setup(a => a.Add(It.IsAny<CriminalRecord>()));
            Assert.ThrowsException<Exception>(
                () =>
                {
                    _criminalRecordService.AddCriminalRecord(criminalRecord);
                });
        }

        [TestMethod]
        public void CreateDocument_ThrowsExceptionForName()
        {
            CriminalRecord criminalRecord = new CriminalRecord { Id = Guid.NewGuid()};
            criminalRecord.Name = "TestName";
            criminalRecord.Type = "Murder";
            criminalRecord.Description = "Minimum 20 chars is required for this Test.";

            DocumentDTO documentDto = new DocumentDTO { };
            _criminalRecordsRepository.Setup(a => a.AddDocument(It.IsAny<Document>(), It.IsAny<Guid>()));
            _documentRepository.Setup(a => a.Add(It.IsAny<Document>()));

            Assert.ThrowsException<Exception>(
                () =>
                {
                    _criminalRecordService.CreateDocument(criminalRecord.Id, documentDto);
                });
        }

        [TestMethod]
        public void DeleteCriminalRecord()
        {
            CriminalRecord criminalRecord = new CriminalRecord { Id = Guid.NewGuid(), Name = "TestCriminalRecord", };
            Guid myGuid = criminalRecord.Id;

            _criminalRecordsRepository.Setup(x => x.Remove(It.IsAny<Guid>())).Callback<Guid>((myGuid)
                => _criminalRecordService.RemoveCriminalRecord(myGuid));                                       
            

        }

    }
}
