using MIS.DataAccess.Abstractions;
using MSI.DataAccess;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.DataAccess
{
    public class EFPolicemanRepository:EFRepository<Policeman>,IEFPolicemanRepository
    {

        public EFPolicemanRepository(PoliceContext context) : base(context)
        {

        }

        public Policeman GetPolicemanByEmail(string email)
        {
            var policeman = _context.Policemen
                .Where(x => x.Email == email)
                .FirstOrDefault();

            return policeman;
        }

    }
}
