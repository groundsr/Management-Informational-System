using Microsoft.AspNetCore.Mvc;
using MIS.BusinessLogic;
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

        private readonly CriminalRecordService _criminalRecordService;
        public CriminalRecordController( CriminalRecordService criminalRecordService)
        {
            _criminalRecordService = criminalRecordService;
        }

        public IActionResult Index(string name)
        {
            IEnumerable<CriminalRecord> result;

            if (name != null)
            {
                result = _criminalRecordService.GetCriminalRecordsByName(name);
                return View(result);
            }
            else
            {
                result = _criminalRecordService.GetAllCriminalRecords().ToList();
            }
            return View(result);
        }


        public int GetCriminalRecordStatus(Guid criminalRecordId)
        {
            CriminalRecord criminalRecord = _criminalRecordService.GetCriminalRecordById(criminalRecordId);
           return( _criminalRecordService.GetStatus(criminalRecord));
        }


        [HttpPost]
        public IActionResult SetStatus(int enumValue, Guid criminalRecordId)
        {
            CriminalRecord criminalRecord = _criminalRecordService.GetCriminalRecordById(criminalRecordId);

            if(enumValue==0)
            {
                _criminalRecordService.DisableStatus(enumValue,criminalRecordId);
            }
            else if(enumValue==1)
            {
                _criminalRecordService.EnableStatus(enumValue, criminalRecordId);
            }
            return RedirectToAction(nameof(Details));
        }

        [HttpPost]
        public IActionResult Create(CriminalRecord criminalRecord)
        {

            if (ModelState.IsValid)
            {
                if (_criminalRecordService.CheckIfRecordExists(criminalRecord))
                {
                    return BadRequest();
                }
                else
                {
                    criminalRecord.CreatedOn = DateTime.Today;
                    criminalRecord.Status = Status.Active;
                    
                    _criminalRecordService.AddCriminalRecord(criminalRecord);

                    CriminalRecordPoliceman criminalRecordPoliceman = new CriminalRecordPoliceman();
                    criminalRecordPoliceman.CriminalRecord = criminalRecord;

                    _criminalRecordService.AddCriminalRecordPoliceman(criminalRecordPoliceman);

                    _criminalRecordService.SaveCriminalRecordPoliceman();

                    _criminalRecordService.SaveCriminalRecord();

                    return RedirectToAction(nameof(Index));
                }

            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public IActionResult Delete(Guid id)    {

            List<CriminalRecordPoliceman> criminalRecordPolicemenList = 
                (List<CriminalRecordPoliceman>)_criminalRecordService.GetCriminalRecordPolicemenById(id);

            for(int i=0;i<criminalRecordPolicemenList.Count;i++)
            {
                _criminalRecordService.RemoveCriminalRecordPoliceman(criminalRecordPolicemenList[i].Id);
            }

            _criminalRecordService.SaveCriminalRecordPoliceman();

            _criminalRecordService.RemoveCriminalRecord(id);

            _criminalRecordService.SaveCriminalRecord();

            return RedirectToAction(nameof(Index));
        
        }

        [HttpPost]
        public IActionResult EditCriminalRecord(CriminalRecord criminalRecord)
        {
            _criminalRecordService.UpdateCriminalRecord(criminalRecord);
            _criminalRecordService.SaveCriminalRecord();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(Guid recordId)
        {
            var criminalRecordDetails = _criminalRecordService.GetCriminalRecordById(recordId);

            IEnumerable<CriminalRecordPoliceman> criminalRecordPolicemen =
                _criminalRecordService.GetAllCriminalRecordsPolicemanForARecord(criminalRecordDetails);

            return View(criminalRecordPolicemen); 
        }

        [HttpPost]
        public IActionResult AddPolicemanToACriminalRecord(string policemanEmail,Guid criminalRecordId )
            {

            Policeman policeman = _criminalRecordService.GetPolicemanByEmail(policemanEmail);
            CriminalRecord criminalRecord = _criminalRecordService.GetCriminalRecordById(criminalRecordId);

            if(ModelState.IsValid)
            {
                _criminalRecordService.AddPolicemanToCriminalRecord(policeman,criminalRecord);
                _criminalRecordService.SaveCriminalRecordPoliceman();
                string redirectString = "https://localhost:44300/CriminalRecord/Details?recordId=" + criminalRecordId;
                return Redirect(redirectString);
            }
            return RedirectToPage(nameof(Details));
            }

        [HttpGet]
        public JsonResult GetAll()
        {
            List<CriminalRecord> criminalRecords = new List<CriminalRecord>();
            criminalRecords = (List<CriminalRecord>)_criminalRecordService.GetAllCriminalRecords();
            return Json(criminalRecords);

        }


    }
}
