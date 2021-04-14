using Microsoft.AspNetCore.Mvc;
using MIS.DataAccess.Abstractions;
using MIS.Model;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIS.Controllers
{
    public class CriminalRecordController:Controller
    {
        private readonly IEFCriminalRecordRepository _efCriminalRecordRepository;
        private readonly IPolicemanRepository _efPolicemanRepository;
        private readonly IEFCriminalRecordPolicemanRepository _efCriminalRecordPolicemanRepository;

        public CriminalRecordController(IEFCriminalRecordRepository efCriminalRecordRepository, IPolicemanRepository efPolicemanRepository, IEFCriminalRecordPolicemanRepository efCriminalRecordPoliceman)
        {
            _efCriminalRecordRepository = efCriminalRecordRepository;
            _efPolicemanRepository = efPolicemanRepository;
            _efCriminalRecordPolicemanRepository = efCriminalRecordPoliceman;
        }

        public IActionResult SearchCriminalRecord()
        {
            return View();
        }

        public IActionResult Index(string name)
        {
            IEnumerable<CriminalRecord> result;

            if (name != null)
            {
                result = _efCriminalRecordRepository.GetCriminalRecordsByName(name).ToList();
                return View(result);
            }
            else
            {
                result = _efCriminalRecordRepository.GetAll().ToList();
            }
            return View(result);

        }

        [HttpPost]
        public IActionResult Create(CriminalRecord criminalRecord)
        {

            if (ModelState.IsValid)
            {
                if (_efCriminalRecordRepository.CheckIfRecordExists(criminalRecord))
                {
                    return BadRequest();
                }
                else
                {
                    criminalRecord.CreatedOn = DateTime.Today;
                    criminalRecord.Status = Status.Active;
                    
                    _efCriminalRecordRepository.Add(criminalRecord);

                    CriminalRecordPoliceman criminalRecordPoliceman = new CriminalRecordPoliceman();
                    criminalRecordPoliceman.CriminalRecord = criminalRecord;

                    _efCriminalRecordPolicemanRepository.Add(criminalRecordPoliceman);
                    _efCriminalRecordPolicemanRepository.Save();

                    _efCriminalRecordRepository.Save();
                    return RedirectToAction(nameof(Index));
                }

            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public IActionResult Delete(Guid id) { 


            List<CriminalRecordPoliceman> criminalRecordPolicemenList =
                _efCriminalRecordPolicemanRepository.GetAll(x => x.CriminalRecord.Id == id)
                .ToList();

            for(int i=0;i<criminalRecordPolicemenList.Count;i++)
            {
                _efCriminalRecordPolicemanRepository.Remove(criminalRecordPolicemenList[i].Id);
            }

            _efCriminalRecordPolicemanRepository.Save();
            _efCriminalRecordRepository.Remove(id);


            _efCriminalRecordRepository.Save();
            return RedirectToAction(nameof(Index));
        
        }

        public IActionResult EditCriminalRecordById(Guid id)
        {
            var record = _efCriminalRecordRepository.Get(id);
            return View(record);
        }

        [HttpPost]
        public IActionResult EditCriminalRecord(CriminalRecord criminalRecord)
        {
            _efCriminalRecordRepository.Update(criminalRecord);
            _efCriminalRecordRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(Guid recordId)
        {
            var criminalRecordDetails = _efCriminalRecordRepository.Get(recordId);

            IEnumerable<CriminalRecordPoliceman> criminalRecordPolicemen = _efCriminalRecordPolicemanRepository
                .GetAll(criminalRecordDetails);

            return View(criminalRecordPolicemen); 
        }

        [HttpPost]
        public IActionResult AddPolicemanToACriminalRecord(string policemanEmail,Guid criminalRecordId )
            {
        
            Policeman policeman = _efPolicemanRepository.GetByEmail(policemanEmail);
            CriminalRecord criminalRecord = _efCriminalRecordRepository.Get(criminalRecordId);

            if(ModelState.IsValid)
            {
                _efCriminalRecordPolicemanRepository.AddPolicemanToCriminalRecord(policeman,criminalRecord);
                _efCriminalRecordPolicemanRepository.Save();
                return Redirect("https://localhost:44300/CriminalRecord");
            }
            return RedirectToPage(nameof(Details));
        }

    }
}
