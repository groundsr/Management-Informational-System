using MIS.DataAccess.Abstractions;
using MSI.DataAccess;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.DataAccess
{
    public class EFPoliceStationRepository : EFRepository<PoliceSection>, IPoliceStationRepository
    {
        private readonly PoliceContext context;

        public EFPoliceStationRepository(PoliceContext context) : base (context)
        {
            this.context = context;
        }


    }
}
