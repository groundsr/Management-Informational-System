using Microsoft.AspNetCore.Mvc;
using MIS.BusinessLogic;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIS.Controllers
{
    public class PoliceSectionController : Controller
    {
        public PoliceSectionService policeSectionService;

        public PoliceSectionController(PoliceSectionService policeStationService)
        {
            this.policeSectionService = policeStationService;
        }

        public IActionResult Index(string searchString)
        {
            var model = policeSectionService.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(s => s.Name.Contains(searchString));
            }
            return View(model);
            //var model = policeSectionService.GetAll();
            //return View(model);
        }

        public IActionResult AddPolicemanToStation(Guid id, string email)
        {
            var policeSection = policeSectionService.Get(id);

            policeSectionService.AddPoliceToSection(policeSection, email);

            return RedirectToAction("Index");

        }

        public IActionResult Hierarchy(Guid id)
        {
            var policeSection = policeSectionService.Get(id);

            return View(policeSection);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PoliceSection policeSection)
        {
            if (ModelState.IsValid)
            {
                policeSectionService.Add(policeSection);
                return RedirectToAction("Index");

            }
            return View(policeSection);
        }

        public IActionResult Update(Guid id)
        {

            
            PoliceSection policeSection = policeSectionService.Get(id);

            if (policeSection == null)
            {
                return NotFound();
            }

            return View(policeSection);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(PoliceSection policeSection)
        {
            
            if (ModelState.IsValid)
            {
                policeSectionService.Update(policeSection);
                return RedirectToAction("Index");

            }
            return View(policeSection);
        }

        public IActionResult Delete(Guid id)
        {


            PoliceSection policeSection = policeSectionService.Get(id);

            if (policeSection == null)
            {
                return NotFound();
            }

            return View(policeSection);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Guid id)
        {
            
                policeSectionService.Delete(id);
                return RedirectToAction("Index");

        }

    }
}
