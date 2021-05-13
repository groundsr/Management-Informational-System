using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIS.BusinessLogic;
using MIS.DataAccess.Abstractions;
using MIS.Model;
using Moq;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.ApplicationLogic.Tests
{
    [TestClass]
    public class MeetingRequestServiceTests
    {
        private readonly Mock<IMeetingRequestRepository> meetingRequestRepository;
        private readonly Mock<IPolicemanRepository> policemanRepository;
        private readonly Mock<IMeetingRequestPolicemanRepository> requestPolicemanRepository;
        private readonly Mock<IMeetingPolicemanRepository> meetingPolicemanRepository;
        private readonly MeetingRequestService meetingRequestService;
        public MeetingRequestServiceTests()
        {
            meetingPolicemanRepository = new Mock<IMeetingPolicemanRepository>();
            meetingRequestRepository = new Mock<IMeetingRequestRepository>();
            requestPolicemanRepository = new Mock<IMeetingRequestPolicemanRepository>();
            policemanRepository = new Mock<IPolicemanRepository>();
        }

        public void CreateRequest_CreatesARequestForAllPolicemen()
        {
            List<string> participantsEmail = new List<string>();
            MeetingRequest meetingRequest = new MeetingRequest() { Id = Guid.NewGuid(), Topic = "Consulting" };
            Policeman policeman1 = new Policeman() { Id = Guid.NewGuid(), Email = "mihnea@mihnea.com", Name = "Mihnea" };
            Policeman policeman2 = new Policeman() { Id = Guid.NewGuid(), Email = "laur@laur.com", Name = "Laur" };
            Policeman policeman3 = new Policeman() { Id = Guid.NewGuid(), Email = "dan@dan.com", Name = "Dan" };
            policemanRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(policeman1);
            List<MeetingRequestPoliceman> meetingRequests = new List<MeetingRequestPoliceman>();
            requestPolicemanRepository.Setup(x => x.Add(It.IsAny<MeetingRequestPoliceman>())).Callback<List<MeetingRequestPoliceman>>
                (x => x.Add(new MeetingRequestPoliceman() {MeetingRequest = meetingRequest ,Policeman = policeman1 }));

            meetingRequestService.CreateRequest(meetingRequest, participantsEmail);

            Assert.IsTrue(meetingRequests.Count == 3);
        }

      
    }
}
