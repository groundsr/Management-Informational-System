using Microsoft.AspNetCore.Mvc;
using MIS.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIS.Controllers
{
    public class PolicemanController : Controller
    {
        private readonly PolicemanService policemanService;

        public PolicemanController(PolicemanService policemanService)
        {
            this.policemanService = policemanService;
        }
        public IActionResult AddSubordinate(Guid policemanId , string email)
        {
            policemanService.AddSubordinate(policemanId, email);
            return Ok();
        }
    }
}
