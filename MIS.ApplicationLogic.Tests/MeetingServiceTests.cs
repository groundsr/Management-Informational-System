using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIS.BusinessLogic;
using MIS.DataAccess.Abstractions;
using MIS.Model;
using Moq;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MIS.ApplicationLogic.Tests
{
    [TestClass]
    public class MeetingServiceTests
    {
        private Mock<IMeetingPolicemanRepository> meetingPolicemanRepository;
        private Mock<IMeetingRepository> meetingRepository;
        private MeetingService meetingService;

        public MeetingServiceTests()
        {
            meetingPolicemanRepository = new Mock<IMeetingPolicemanRepository>();
            meetingRepository = new Mock<IMeetingRepository>();
            meetingService = new BusinessLogic.MeetingService(meetingRepository.Object, meetingPolicemanRepository.Object);
        }
        [TestMethod]
        public void GetCurrentMonthMeetings_ReturnsMeetingGroupedByDay()
        {
            //arrange
            Policeman policeman = new Policeman() { Email = "mihnea@mihnea.com", Age = 21, Id = Guid.NewGuid() };
            Meeting meeting1 = new Meeting() { Id = Guid.NewGuid(), Topic = "Consulting", Start = DateTime.Now, End = DateTime.Now };
            meeting1.Start = meeting1.Start.AddDays(3);
            meeting1.End = meeting1.End.AddDays(3);
            Meeting meeting2 = new Meeting() { Id = Guid.NewGuid(), Topic = "Consulting", Start = DateTime.Now, End = DateTime.Now };
            meeting2.Start = meeting2.Start.AddDays(1);
            meeting2.End = meeting2.End.AddDays(1);
            Meeting meeting3 = new Meeting() { Id = Guid.NewGuid(), Topic = "Consulting", Start = DateTime.Now, End = DateTime.Now };
            meeting3.Start = meeting3.Start.AddDays(2);
            meeting3.End = meeting3.End.AddDays(2);

            MeetingPoliceman meetingPoliceman1 = new MeetingPoliceman() { Id = Guid.NewGuid(), Policeman = policeman, Meeting = meeting1 };
            MeetingPoliceman meetingPoliceman2 = new MeetingPoliceman() { Id = Guid.NewGuid(), Policeman = policeman, Meeting = meeting2 };
            MeetingPoliceman meetingPoliceman3 = new MeetingPoliceman() { Id = Guid.NewGuid(), Policeman = policeman, Meeting = meeting3 };
            
            List<MeetingPoliceman> meetings = new List<MeetingPoliceman>();
            meetings.Add(meetingPoliceman1);
            meetings.Add(meetingPoliceman2);
            meetings.Add(meetingPoliceman3);
            meetingPolicemanRepository.Setup(x => x.GetAllForPoliceman(It.IsAny<Policeman>())).Returns(meetings);

            //act
            var groupedMeetings = meetingService.GetCurrentMonthMeetings(policeman);

            //assert
            if (groupedMeetings != null && groupedMeetings.Count() > 1)
            {
                for (int i = 1; i < groupedMeetings.Count(); i++)
                {
                    Assert.AreNotEqual(groupedMeetings.ElementAt(i).ElementAt(0).Start.Day, groupedMeetings.ElementAt(i - 1).ElementAt(0).Start.Day);
                }
            }
        }
        [TestMethod]
        public void GetById_ReturnsTheCorrectOfficer()
        {
            Meeting meeting = new Meeting() { Topic = "Consulting", Id = Guid.NewGuid() };
            meetingRepository.Setup(x => x.Get(It.IsAny<Guid>())).Returns(meeting);
            Meeting returnedMeeting = new Meeting();

            returnedMeeting = meetingService.GetById(meeting.Id);

            Assert.AreEqual(meeting, returnedMeeting);
        }

        [TestMethod]
        public void GetPreviousMeeting_ReturnsTheMostRecentPassedMeeting()
        {
            Meeting meeting1 = new Meeting() { Topic = "Consulting", Id = Guid.NewGuid() , Start =DateTime.Now , End = DateTime.Now };
            Meeting meeting2 = new Meeting() { Topic = "Consulting", Id = Guid.NewGuid() , Start =DateTime.Now , End = DateTime.Now };
            Meeting meeting3 = new Meeting() { Topic = "Consulting", Id = Guid.NewGuid() , Start =DateTime.Now , End = DateTime.Now };
            meeting1.Start = meeting1.Start.AddDays(1);
            meeting1.End = meeting1.End.AddDays(1);
            meeting2.Start = meeting2.Start.AddDays(-1);
            meeting2.End = meeting2.End.AddDays(-1);
            meeting2.End = meeting2.End.AddHours(1);
            meeting3.Start = meeting3.Start.AddDays(-2);
            meeting3.End = meeting3.End.AddDays(-2);
            meeting3.End = meeting3.End.AddHours(1);
            Policeman policeman = new Policeman() { Id = Guid.NewGuid(), Age = 21, Email = "mihnea@mihnea.com" };
            List<MeetingPoliceman> policemanMeetings = new List<MeetingPoliceman>();
            policemanMeetings.Add(new MeetingPoliceman() { Meeting = meeting1, Policeman = policeman });
            policemanMeetings.Add(new MeetingPoliceman() { Meeting = meeting2, Policeman = policeman });
            policemanMeetings.Add(new MeetingPoliceman() { Meeting = meeting3, Policeman = policeman });
            meetingPolicemanRepository.Setup(x => x.GetAllForPoliceman(It.IsAny<Policeman>())).Returns(policemanMeetings);

            var previousMeeting = meetingService.GetPreviousMeeting(policeman);

            Assert.AreSame(meeting2, previousMeeting);
        }
    }
}
