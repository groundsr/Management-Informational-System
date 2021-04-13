using Microsoft.AspNetCore.Mvc;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIS.Controllers
{
    public class MeetingRequestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(MeetingRequest meetingRequest , List<string> participants , string test)
        {

            return Ok();
        }

    }
}
