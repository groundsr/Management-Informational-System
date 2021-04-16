using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MIS.BusinessLogic;
using MIS.SignalR;
using MIS.ViewModels.Meeting;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIS.Controllers
{
    public class MeetingController : Controller
    {
        private readonly IHubContext<ChatHub> hubcontext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly PolicemanService policemanService;
        private readonly MeetingService meetingService;

        public MeetingController(IHubContext<ChatHub> hubcontext, UserManager<IdentityUser> userManager,
            PolicemanService policemanService, MeetingService meetingService)
        {
            this.hubcontext = hubcontext;
            this.userManager = userManager;
            this.policemanService = policemanService;
            this.meetingService = meetingService;
        }
        public IActionResult Index()
        {
            var user = userManager.GetUserAsync(User).GetAwaiter().GetResult();
            var policeman = policemanService.GetByUserId(user.Id);
            var model = meetingService.GetCurrentMonthMeetings(policeman);
            return View(model);
        }
        public IActionResult Chat(Guid id)
        {
            Meeting meeting = meetingService.GetById(id);
            var user = userManager.GetUserAsync(User).GetAwaiter().GetResult();
            ChatViewModel model = new ChatViewModel() { Id = user.Id, Name = user.UserName, End = meeting.End, Topic = meeting.Topic };
            return View(model);
        }

        public async Task<IActionResult> SendMessageAsync(string content, string name, string id)
        {
            var user = userManager.GetUserAsync(User).GetAwaiter().GetResult();
            await hubcontext.Clients.All.SendAsync("ReceiveMessage", content, name, id);
            return Ok();
        }

        public IEnumerable<int> GetMeetingsDay()
        {
            var user = userManager.GetUserAsync(User).GetAwaiter().GetResult();
            var policeman = policemanService.GetByUserId(user.Id);
            var model = meetingService.GetCurrentMonthMeetings(policeman).Select(x => x.ElementAt(0).Start.Day);
            return model;
        }
        public IEnumerable<Meeting> GetMeetingsForDay(int day)
        {
            var user = userManager.GetUserAsync(User).GetAwaiter().GetResult();
            var policeman = policemanService.GetByUserId(user.Id);
            var model = meetingService.GetCurrentMonthMeetings(policeman).Where(x => 
            x.ElementAt(0).Start.Day == day).FirstOrDefault();
            return model;
        }
    }
}
