using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MIS.BusinessLogic;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIS.Controllers
{
    public class MeetingRequestController : Controller
    {
        private readonly MeetingRequestService meetingRequestService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly PolicemanService policemanService;

        public MeetingRequestController(MeetingRequestService meetingRequestService , UserManager<IdentityUser>
            userManager , PolicemanService policemanService)
        {
            this.meetingRequestService = meetingRequestService;
            this.userManager = userManager;
            this.policemanService = policemanService;
        }

        public IActionResult Index()
        {
            IEnumerable<MeetingRequest> model = meetingRequestService.GetAll();
            return PartialView("_Index" , model);
        }
        [HttpPost]
        public IActionResult Add(MeetingRequest meetingRequest , List<string> participants)
        {
            var user = userManager.GetUserAsync(User).GetAwaiter().GetResult();
            var policeman = policemanService.GetByUserId(user.Id);
            meetingRequest.Requester = policeman;
            meetingRequestService.Add(meetingRequest);
            participants.Add(policeman.Email);
            meetingRequestService.CreateRequest(meetingRequest, participants);
            return RedirectToAction("Index" , "Meeting");
        }
        public IActionResult DeclineRequest(Guid requestId)
        {
            meetingRequestService.DeclineRequest(requestId);
            return RedirectToAction("Index");
        }
        public IActionResult AcceptRequest(Guid requestId)
        {
            meetingRequestService.AcceptRequest(requestId);
            return RedirectToAction("Index");
        }
    }
}
