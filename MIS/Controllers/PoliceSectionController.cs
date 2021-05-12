using Microsoft.AspNetCore.Mvc;
using MIS.BusinessLogic;
using MIS.BusinessLogic.Filtering;
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
        public PoliceSectionService _policeSectionService;
        private readonly CriminalRecordService _criminalRecordService;

        public PoliceSectionController(PoliceSectionService policeStationService, CriminalRecordService criminalRecordService)
        {
            this._policeSectionService = policeStationService;
            _criminalRecordService = criminalRecordService;
        }

        public IActionResult Index(SearchFilter searchFilter)
        
        {
            return View(_policeSectionService.GetPoliceSectionsByName(searchFilter));
        }

        public IActionResult AddPolicemanToStation(Guid id, string email)
        {
            var policeSection = _policeSectionService.Get(id);
            _policeSectionService.AddPoliceToSection(policeSection, email);
            return RedirectToAction("Index");
        }

        

        // public IActionResult Hierarchy(Guid id)
        public IEnumerable<CriminalRecord> GetCriminalRecordsByName(string name)
        {
            return (_criminalRecordService.GetCriminalRecordsByName(name));
        }

        public IActionResult Hierarchy(Guid id,SearchFilter searchedRecord)
        {
            PoliceSection policeSection = _policeSectionService.Get(id);
            IEnumerable<CriminalRecord> criminalRecords = _policeSectionService.GetCriminalRecordsBySection(id);

            if (searchedRecord.Term != null)
            {
                IEnumerable<CriminalRecord> filteredCriminalRecords = _policeSectionService.GetCriminalRecordsByNameBySection(id, searchedRecord);
                ViewData["criminalRecords"] =filteredCriminalRecords;
            }
            else
            {
                ViewData["criminalRecords"] = criminalRecords;
            }
            return View(_policeSectionService.Get(id));
        }

        

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PoliceSection policeSection, Guid Id, string email)
        {
            if (ModelState.IsValid)
            {

                // policeSectionService.Add(policeSection);
                //Guid guid = policeSectionService.GetPoliceSectionId(policeSection);
                _policeSectionService.Add(policeSection);
                AddPolicemanToStation(policeSection.Id, email);
                return RedirectToAction("Index");

            }
            return View(policeSection);
        }

        public List<Address> GetSectionsAddress()
        {
            return _policeSectionService.GetSectionsAddress();
        }

        public IActionResult Update(Guid id)
        {


            PoliceSection policeSection = _policeSectionService.Get(id);

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
                _policeSectionService.Update(policeSection);
                return RedirectToAction("Index");

            }
            return View(policeSection);
        }

        //public IActionResult Delete(Guid id)
        //{


        //    PoliceSection policeSection = _policeSectionService.Get(id);

        //    if (policeSection == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(policeSection);

        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Guid id)
        {
            _policeSectionService.Delete(id);
            return RedirectToAction("Index");
        }

        //there are some problems when converting dictionary into json so I am gonna return a list of lists
        //where in each list the first item is a dictionary key and the following elements are the value for
        //that specific key
        public List<List<Policeman>> PolicemenHierarchy(Guid sectionId)
        {
            var dictionaryHierarch = _policeSectionService.PolicemenHierarchy(sectionId);
            List<List<Policeman>> leveledHierarchy = new List<List<Policeman>>();
            foreach (var key in dictionaryHierarch.Keys)
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
