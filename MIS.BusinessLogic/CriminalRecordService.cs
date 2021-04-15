using MIS.DataAccess.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.BusinessLogic
{
    public class CriminalRecordService
    {
        private readonly IEFCriminalRecordRepository _criminalRecord;
        private readonly IEFCriminalRecordPolicemanRepository _criminalRecordPoliceman;
        private readonly IPolicemanRepository _policemanRepository;

        public CriminalRecordService(IEFCriminalRecordRepository criminalRecord
            , IEFCriminalRecordPolicemanRepository criminalRecordPoliceman
            , IPolicemanRepository policemanRepository)
        {
            _criminalRecord = criminalRecord;
            _criminalRecordPoliceman = criminalRecordPoliceman;
            _policemanRepository = policemanRepository;
        }


    }
}
