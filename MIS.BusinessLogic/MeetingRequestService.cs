using MIS.DataAccess.Abstractions;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.BusinessLogic
{
    public class MeetingRequestService
    {
        private readonly IMeetingRequestRepository meetingRequestRepository;
        private readonly IPolicemanRepository policemanRepository;
        private readonly IMeetingRequestPolicemanRepository requestPolicemanRepository;

        public MeetingRequestService(IMeetingRequestRepository meetingRequestRepository , 
            IPolicemanRepository policemanRepository , IMeetingRequestPolicemanRepository requestPolicemanRepository)
        {
            this.meetingRequestRepository = meetingRequestRepository;
            this.policemanRepository = policemanRepository;
            this.requestPolicemanRepository = requestPolicemanRepository;
        }

        public void CreateRequest(MeetingRequest meetingRequest , List<string> participants)
        {
            foreach(var email in participants)
            {
                Policeman policeman = policemanRepository.GetByEmail(email);
                requestPolicemanRepository.Add(new Model.MeetingRequestPoliceman() 
                { Policeman = policeman, MeetingRequest = meetingRequest });
            }
        }

        public void Add(MeetingRequest meetingRequest)
        {
            meetingRequestRepository.Add(meetingRequest);
        }
    }
}
