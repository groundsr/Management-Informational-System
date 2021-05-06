using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIS.BusinessLogic;
using MIS.BusinessLogic.Filtering;
using MIS.DataAccess.Abstractions;
using MIS.DTOs.BusinessLogic;
using MIS.Model;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MIS.Controllers
{
    public class CriminalRecordController : Controller
    {

        private readonly CriminalRecordService _criminalRecordService;
        private readonly IHostingEnvironment _env;

        public CriminalRecordController(CriminalRecordService criminalRecordService, IHostingEnvironment env)
        {
            _criminalRecordService = criminalRecordService;
            _env = env;
        }

        public IActionResult Index(SearchFilter searchEngine)
        
        
        {
            return View(_criminalRecordService.SearchUsingEngine(searchEngine));
        }

        public int GetCriminalRecordStatus(Guid criminalRecordId)
        {
            CriminalRecord criminalRecord = _criminalRecordService.GetCriminalRecordById(criminalRecordId);
            return (_criminalRecordService.GetStatus(criminalRecord));
        }


        public IActionResult SetStatus(int enumValue, Guid criminalRecordId)
        {
            CriminalRecord criminalRecord = _criminalRecordService.GetCriminalRecordById(criminalRecordId);

            if (enumValue == 0)
            {
                _criminalRecordService.DisableStatus(enumValue, criminalRecordId);
            }
            else if (enumValue == 1)
            {
                _criminalRecordService.EnableStatus(enumValue, criminalRecordId);
            }
            return RedirectToAction("Details", new { recordId = criminalRecordId });
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
                    criminalRecord.CreatedOn = DateTime.Now;
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
        public IActionResult Delete(Guid id)
        {

            List<CriminalRecordPoliceman> criminalRecordPolicemenList =
                (List<CriminalRecordPoliceman>)_criminalRecordService.GetCriminalRecordPolicemenById(id);

            for (int i = 0; i < criminalRecordPolicemenList.Count; i++)
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

   
        [HttpPost]
        public JsonResult ModifyType(string newType, Guid criminalRecordId)
         {
            _criminalRecordService.ModifyType(newType, criminalRecordId);
            return Json(newType);
        }

        public IActionResult DeleteDocument(Guid criminalRecordId,Guid documentId)
        {

            if (documentId!=null)
            {
                _criminalRecordService.DeleteDocument(documentId);
            }

           return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult FileInModel(Guid criminalRecordId, DocumentDTO documentDTO)
        {
            _criminalRecordService.UploadFile(criminalRecordId, documentDTO);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(Guid recordId)
        {
            var criminalRecordDetails = _criminalRecordService.GetCriminalRecordById(recordId);

            IEnumerable<CriminalRecordPoliceman> criminalRecordPolicemen =
                _criminalRecordService.GetAllCriminalRecordsPolicemanForARecord(criminalRecordDetails);

            ViewData["documents"] = _criminalRecordService.GetDocuments(recordId);

            return View(criminalRecordPolicemen);
        }

        [HttpPost]
        public IActionResult AddPolicemanToACriminalRecord(string policemanEmail, Guid criminalRecordId)
        {

            Policeman policeman = _criminalRecordService.GetPolicemanByEmail(policemanEmail);
            CriminalRecord criminalRecord = _criminalRecordService.GetCriminalRecordById(criminalRecordId);

            if (ModelState.IsValid)
            {
                _criminalRecordService.AddPolicemanToCriminalRecord(policeman, criminalRecord);
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
