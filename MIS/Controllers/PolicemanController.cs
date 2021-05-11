using Microsoft.AspNetCore.Mvc;
using MIS.BusinessLogic;
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

        public IActionResult Index(Policeman policeman)
        {
            var policemen = policemanService.GetAll();
            return View(policemen);
        }

        //public IActionResult GetPolicemen()
        //{
        //    var policemen = policemanService.GetAll();
        //    return View(policemen);
        //}

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

        [HttpPost]
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
