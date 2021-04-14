using MIS.DataAccess.Abstractions;
using MIS.Model;
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
        private readonly IMeetingPolicemanRepository meetingPolicemanRepository;

        public MeetingRequestService(IMeetingRequestRepository meetingRequestRepository , 
            IPolicemanRepository policemanRepository , IMeetingRequestPolicemanRepository requestPolicemanRepository,
            IMeetingPolicemanRepository meetingPolicemanRepository)
        {
            this.meetingRequestRepository = meetingRequestRepository;
            this.policemanRepository = policemanRepository;
            this.requestPolicemanRepository = requestPolicemanRepository;
            this.meetingPolicemanRepository = meetingPolicemanRepository;
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

        public IEnumerable<MeetingRequest> GetAll()
        {
            return meetingRequestRepository.GetAll();
        }
        public void Remove(Guid requestId)
        {
            meetingRequestRepository.Remove(requestId);
        }
        public void Add(MeetingRequest meetingRequest)
        {
            meetingRequestRepository.Add(meetingRequest);
        }

        public void DeclineRequest(Guid requestId)
        {
            var request = GetById(requestId);
            requestPolicemanRepository.RemoveAll(request);
            Remove(requestId);
        }

        public MeetingRequest GetById(Guid requestId)
        {
            return meetingRequestRepository.Get(requestId);
        }

        public void AcceptRequest(Guid requestId)
        {
            var request = GetById(requestId);
            Meeting meeting = new Meeting() { Topic = request.Topic , Start = request.StartDate , End = request.EndDate};
            IEnumerable<Policeman> policemen = requestPolicemanRepository.GetPolicemanForRequest(request);
            foreach(var it in policemen)
            {
                MeetingPoliceman meetingPoliceman = new MeetingPoliceman() { Meeting = meeting, Policeman = it };
                meetingPolicemanRepository.Add(meetingPoliceman);
            }
            DeclineRequest(requestId);
        }
    }
}
