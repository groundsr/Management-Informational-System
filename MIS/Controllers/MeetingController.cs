using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MIS.SignalR;
using MIS.ViewModels.Meeting;
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

        public MeetingController(IHubContext<ChatHub> hubcontext, UserManager<IdentityUser> userManager)
        {
            this.hubcontext = hubcontext;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Chat()
        {
            var user = userManager.GetUserAsync(User).GetAwaiter().GetResult();
            ChatUserViewModel model = new ChatUserViewModel() { Id = user.Id, Name = user.UserName };
            return View(model);
        }

        public async Task<IActionResult> SendMessageAsync(string content, string name, string id)
        {
            var user = userManager.GetUserAsync(User).GetAwaiter().GetResult();
            await hubcontext.Clients.All.SendAsync("ReceiveMessage", content, name, id);
            return Ok();
        }
        public async Task<IActionResult> UserConnectedAsync(int lastConnected)
        {
            await hubcontext.Clients.All.SendAsync("UserConnected" , lastConnected);
            return Ok();
        }

    }
}
