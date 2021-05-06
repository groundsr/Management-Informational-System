using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MIS.DataAccess.Abstractions;
using MIS.Model;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MIS;
using MIS.DTOs.BusinessLogic;
using MIS.BusinessLogic.Filtering;
using Microsoft.Extensions.DependencyInjection;

namespace MIS.BusinessLogic
{
    public class CriminalRecordService
    {
        private readonly ICriminalRecordRepository _criminalRecord;
        private readonly ICriminalRecordPolicemanRepository _criminalRecordPoliceman;
        private readonly IPolicemanRepository _policemanRepository;
        private readonly IPoliceSectionRepository _policeSectionRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IHostingEnvironment _env;
        private  CriminalRecordSearchEngine _searchEngine;

        public CriminalRecordService(ICriminalRecordRepository criminalRecord
            , ICriminalRecordPolicemanRepository criminalRecordPoliceman
            , IPolicemanRepository policemanRepository
            , IHostingEnvironment env, IDocumentRepository documentRepository
            , IPoliceSectionRepository policeSectionRepository)
        {
            _criminalRecord = criminalRecord;
            _criminalRecordPoliceman = criminalRecordPoliceman;
            _policemanRepository = policemanRepository;
            _documentRepository = documentRepository;
            _env = env;
            _policeSectionRepository = policeSectionRepository;

        }

        public int IdWrapper { get; set; }

        public IEnumerable<CriminalRecord> GetAllCriminalRecords()
        {
            return _criminalRecord.GetAll();
        }


        public CriminalRecordPoliceman GetCriminalRecordPoliceman(CriminalRecord criminalRecord)
        {
            var criminalRecordPoliceman = _criminalRecordPoliceman.GetCriminalRecordPoliceman(criminalRecord);

            return criminalRecordPoliceman;
        }

        public void AddPolicemanToCriminalRecord(Policeman policeman, CriminalRecord criminalRecord)
        {
            var recordPoliceman = _criminalRecordPoliceman.GetCriminalRecordPoliceman(criminalRecord);
            recordPoliceman.CriminalRecord = criminalRecord;

            if (recordPoliceman.Policeman == null)
            {
                recordPoliceman.Policeman = policeman;
                _criminalRecordPoliceman.Update(recordPoliceman);
            }
            else
            {
                var newRecordPoliceman = new CriminalRecordPoliceman();
                newRecordPoliceman.CriminalRecord = criminalRecord;
                newRecordPoliceman.Policeman = policeman;


                if (!(_criminalRecordPoliceman.CheckIfPolicemanWasAdded(newRecordPoliceman)))
                {
                    _criminalRecordPoliceman.Add(newRecordPoliceman);
                }

            }
            _criminalRecordPoliceman.Save();
        }



        public void AddCriminalRecord(CriminalRecord criminalRecord)
        {
            _criminalRecord.Add(criminalRecord);
        }

        public IEnumerable<CriminalRecordPoliceman> GetCriminalRecordPolicemenById(Guid id)
        {
            return _criminalRecordPoliceman.GetCriminalRecordPolicemenByRecordId(id);
        }

        public IEnumerable<CriminalRecordPoliceman> GetAllCriminalRecordsPoliceman(CriminalRecord criminalRecord)
        {
            List<CriminalRecordPoliceman> criminalRecordPolicemen =
                (List<CriminalRecordPoliceman>)_criminalRecordPoliceman.GetAll();

            return criminalRecordPolicemen;
        }

        public void SaveCriminalRecord()
        {
            _criminalRecord.Save();
        }      
        public void SaveCriminalRecordPoliceman()
        {
            _criminalRecordPoliceman.Save();
        }   
        public void SavePoliceman()
        {
            _policemanRepository.Save();
        }

        public IEnumerable<Document> GetDocuments(Guid criminalRecordId)
        {
            return _criminalRecord.GetDocuments(criminalRecordId);
        }

        public void AddCriminalRecordPoliceman(CriminalRecordPoliceman criminalRecordPoliceman)
        {
            _criminalRecordPoliceman.Add(criminalRecordPoliceman);
        }

        public IEnumerable<CriminalRecord> GetCriminalRecordByPolicemanName(string policemanName)
        {
            return (_criminalRecordPoliceman.GetCriminalRecordsByPolicemanName(policemanName));
        }

        public IEnumerable<CriminalRecordPoliceman> GetAllCriminalRecordsPolicemanForARecord(CriminalRecord criminalRecordDetails)
        {
            return (_criminalRecordPoliceman.GetAllCriminalRecordsPolicemanForARecord(criminalRecordDetails));
        }

        public bool CheckIfRecordExists(CriminalRecord criminalRecord)
        {
            return (_criminalRecord.CheckIfRecordExists(criminalRecord));
        }

        public IEnumerable<CriminalRecord> SearchUsingEngine(SearchFilter searchFilter)
        {
            List<CriminalRecord> criminalRecords = (List<CriminalRecord>)_criminalRecord.GetAll();
            _searchEngine = new CriminalRecordSearchEngine(criminalRecords, _criminalRecord, _criminalRecordPoliceman);
            return( _searchEngine.Search(searchFilter));
        }

        public void RemoveCriminalRecordPoliceman(Guid id)
        {
            _criminalRecordPoliceman.Remove(id);
        }

        public void RemoveCriminalRecord(Guid id)
        {
            _criminalRecord.Remove(id);
        }

        public void ModifyType(string newType,Guid CriminalRecordId)
        {
            CriminalRecord criminalRecord = _criminalRecord.Get(CriminalRecordId);

            criminalRecord.Type = newType;
            _criminalRecord.Save();
        }

        public IEnumerable<CriminalRecord> FilterCriminalRecords(int filterValue)
        {
            List<CriminalRecord> criminalRecords = (List<CriminalRecord>)GetAllCriminalRecords();
            List<CriminalRecord> filteredRecords = new List<CriminalRecord>();

            if (filterValue == 1)
            {
                foreach (var item in criminalRecords)
                {
                    if (item.Status == Status.Active)
                    {
                        filteredRecords.Add(item);
                    }
                }
            }
            else if(filterValue==0)
            {
                foreach(var item in criminalRecords)
                {
                    if(item.Status==Status.Closed)
                    {
                        filteredRecords.Add(item);
                    }
                }
            }

            return filteredRecords; 
        }
        public void UpdateCriminalRecord(CriminalRecord criminalRecord)
        {
            _criminalRecord.Update(criminalRecord);
        }

        public CriminalRecord GetCriminalRecordById(Guid id)
        {
            return(_criminalRecord.Get(id));
        }

        public Policeman GetPolicemanByEmail(string email)
        {
           return( _policemanRepository.GetByEmail(email));
        }

        public void EnableStatus(int enumValue,Guid criminalRecordId)
        {
            CriminalRecord record=_criminalRecord.Get(criminalRecordId);
            record.Status = Status.Active;
            _criminalRecord.Update(record);
            _criminalRecord.Save();
        }

        public void DisableStatus(int enumValue,Guid criminalRecordId)
        {
            CriminalRecord record = _criminalRecord.Get(criminalRecordId);

            record.Status = Status.Closed;
            _criminalRecord.Update(record);
            _criminalRecord.Save();
        }

        public int GetStatus(CriminalRecord criminalRecord)
        {
           return  _criminalRecord.GetStatus(criminalRecord);
        }

        public void UploadFile(Guid criminalRecordId,DocumentDTO documentDTO)
        {
            var dir = _env.ContentRootPath;
            
            var webRootParg = _env.WebRootPath;

            string path = Path.Combine(webRootParg,"Images");
                        string documentPath="~/Images/"+ $"{documentDTO.Name}.png";



            var fileStream = new FileStream(Path.Combine(path, $"{documentDTO.Name}.png"),
                             FileMode.Create,
                             FileAccess.Write);
            
            documentDTO.File.CopyTo(fileStream);
            documentDTO.Path =Path.Combine(path,documentDTO.Name);
            documentDTO.Path = documentPath;
            CreateDocument(criminalRecordId,documentDTO);
            
        }

        public IEnumerable<CriminalRecord> GetCriminalRecordsByName(string name)
        {
           return( _criminalRecord.GetCriminalRecordsByName(name));
        }

        public void DeleteDocument(Guid documentId)
        {   
            _documentRepository.Remove(documentId);
            _documentRepository.Save();
        }


        public void CreateDocument(Guid criminalRecordId,DocumentDTO documentDTO)
        {
            Document document = new Document();

            document.Name = documentDTO.Name;
            document.AddedDate = DateTime.Now;
            document.Path = documentDTO.Path;

            _documentRepository.Add(document);
            _documentRepository.Save();

            _criminalRecord.AddDocument(document,criminalRecordId);
            _criminalRecord.Save();

        }


    }
}
