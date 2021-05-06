using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.Tests
{
    [TestClass]
    public class MeetingTests
    {
        [TestMethod]
        public void IsValid_ReturnsTrueForValidMeeting()
        {
            Meeting meeting = new Meeting() { Id = Guid.NewGuid(), Topic = "Consulting" };
            meeting.Start = DateTime.Now;
            meeting.Start = meeting.Start.AddDays(1);
            meeting.End = DateTime.Now;
            meeting.End = meeting.End.AddDays(1);
            meeting.End = meeting.End.AddHours(1);

            Assert.IsTrue(meeting.IsValid());
        }
        public void IsValid_ReturnsFalseForInvalidEndDayEqualsStartDay()
        {
            Meeting meeting = new Meeting() { Id = Guid.NewGuid(), Topic = "Consulting" };
            meeting.Start = DateTime.Now;
            meeting.Start = meeting.Start.AddDays(-1);
            meeting.End = DateTime.Now;
            meeting.End = meeting.End.AddDays(-1);
            meeting.End = meeting.End.AddHours(1);

            Assert.IsFalse(meeting.IsValid());
        }
        [TestMethod]
        public void IsValid_ReturnsFalseForInvalidEndHourMeeting()
        {
            Meeting meeting = new Meeting() { Id = Guid.NewGuid(), Topic = "Consulting" };
            meeting.Start = DateTime.Now;
            meeting.Start = meeting.Start.AddDays(-1);
            meeting.End = DateTime.Now;
            meeting.End = meeting.End.AddDays(-1);
            meeting.End = meeting.End.AddHours(-1);

            Assert.IsFalse(meeting.IsValid());
        }
        [TestMethod]
        public void IsValid_ReturnsFalseForInvalidEndDaySmallerStartDay()
        {
            Meeting meeting = new Meeting() { Id = Guid.NewGuid(), Topic = "Consulting" };
            meeting.Start = DateTime.Now;
            meeting.Start = meeting.Start.AddDays(-1);
            meeting.End = DateTime.Now;
            meeting.End = meeting.End.AddDays(-2);
            meeting.End = meeting.End.AddHours(1);

            Assert.IsFalse(meeting.IsValid());
        }
    }
}
