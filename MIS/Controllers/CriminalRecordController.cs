using Microsoft.AspNetCore.Mvc;
using MIS.DataAccess.Abstractions;
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
        private bool isSearched = false;
        public CriminalRecordController(IEFCriminalRecordRepository efCriminalRecordRepository)
        {
            _efCriminalRecordRepository = efCriminalRecordRepository;
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

    }
}
