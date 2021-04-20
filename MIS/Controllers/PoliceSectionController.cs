using Microsoft.AspNetCore.Mvc;
using MIS.BusinessLogic;
using MSI.Model;
using Newtonsoft.Json;
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
            return View(policeSectionService.Get(id));
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

        //there are some problems when converting dictionary into json so I am gonna return a list of lists
        //where in each list the first item is a dictionary key and the following elements are the value for
        //that specific key
        public List<List<Policeman>> PolicemenHierarchy(Guid sectionId)
        {
            var dictionaryHierarch = policeSectionService.PolicemenHierarchy(sectionId);
            List<List<Policeman>> leveledHierarchy = new List<List<Policeman>>();
            foreach(var key in dictionaryHierarch.Keys)
            {
                List<Policeman> current = new List<Policeman>();
                current.Add(key);
                current.AddRange(dictionaryHierarch[key]);
                leveledHierarchy.Add(current);
            }
            return leveledHierarchy;
        }

    }
}
