using MIS.DataAccess.Abstractions;
using MIS.Model;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.BusinessLogic
{
    public class MeetingService
    {
        private readonly IMeetingRepository meetingRepository;
        private readonly IMeetingPolicemanRepository meetingPolicemanRepository;

        public MeetingService(IMeetingRepository meetingRepository , IMeetingPolicemanRepository 
            meetingPolicemanRepository)
        {
            this.meetingRepository = meetingRepository;
            this.meetingPolicemanRepository = meetingPolicemanRepository;
        }
        public IEnumerable<IEnumerable<Meeting>> GetAllForPoliceman(Policeman policeman)
        {
            var meetings = meetingPolicemanRepository.GetAllForPoliceman(policeman).Select(x => x.Meeting).
                Where(x => x.End >= DateTime.Now).OrderBy(x => x.Start).ThenBy(x => x.End).ToList();
            List<List<Meeting>> meetingsByDate = new List<List<Meeting>>();
            List<Meeting> currentDayMeetings = new List<Meeting>();
            currentDayMeetings.Add(meetings[0]);
            for(int i = 1; i < meetings.Count(); i++)
            {
                if(meetings[i].Start.Day == meetings[i - 1].Start.Day)
                {
                    currentDayMeetings.Add(meetings[i]);
                }
                else
                {
                    meetingsByDate.Add(currentDayMeetings);
                    currentDayMeetings = new List<Meeting>();
                    currentDayMeetings.Add(meetings[i]);
                }
            }
            if(currentDayMeetings.Count != 0)
                meetingsByDate.Add(currentDayMeetings);
            return meetingsByDate.AsEnumerable();
        }

        public Meeting GetById(Guid id)
        {
            return meetingRepository.Get(id);
        }
    }
}
