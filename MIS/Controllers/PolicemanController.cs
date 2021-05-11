using Microsoft.AspNetCore.Mvc;
using MIS.BusinessLogic;
using MIS.BusinessLogic.Filtering;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIS.Controllers
{
    public class PolicemanController : Controller
    {
        private readonly PolicemanService policemanService;
        private readonly PoliceSectionService policeSectionService;

        public PolicemanController(PolicemanService policemanService, PoliceSectionService policeSectionService)
        {
            this.policemanService = policemanService;
            this.policeSectionService = policeSectionService;
        }

        public IActionResult Index(SearchFilter filter)
        {
            return View(policemanService.SearchUsingEngine(filter));
        }

        [HttpPost]
        public IActionResult Create(Policeman policeman)
        {
            if (ModelState.IsValid)
            {
                policemanService.Add(policeman);
                return RedirectToAction("Index");
            }
            return View(policeman);
        }
        [HttpPost]
        public IActionResult Update(Policeman policeman)
        {
            if(ModelState.IsValid)
            {
                policemanService.Update(policeman);              
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid id)
        {   
            policemanService.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult AddSubordinate(Guid policemanId, string email, Guid sectionId)
        {
            Policeman policeman = policemanService.Get(policemanId);
            Policeman subordinate = policemanService.GetByEmail(email);
            if (policeSectionService.BelongToTheSameStation(policeman, subordinate, sectionId))
            {
                policemanService.AddSubordinate(policemanId, email);
            }
            return Ok();
        }
    }
}
