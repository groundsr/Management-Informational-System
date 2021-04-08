using Microsoft.AspNetCore.Mvc;
using MIS.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIS.Controllers
{
    public class PoliceSectionController : Controller
    {
        public PoliceStationService policeStationService;

        public PoliceSectionController(PoliceStationService policeStationService)
        {
            this.policeStationService = policeStationService;
        }

        public IActionResult Index()
        {
            var model = policeStationService.GetAll();
            return View(model);

        }
    }
}
