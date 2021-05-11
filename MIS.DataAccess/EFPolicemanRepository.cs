using MIS.DataAccess.Abstractions;
using MSI.DataAccess;
using MSI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.DataAccess
{
    public class EFPolicemanRepository : EFRepository<Policeman>, IPolicemanRepository
    {
        private readonly PoliceContext context;

        public EFPolicemanRepository(PoliceContext context) : base(context)
        {
            this.context = context;
        }



        public Policeman GetByEmail(string email)
        {
            return context.Policemen.Where(x => x.Email == email).FirstOrDefault();
        }

        public Policeman GetByUserId(Guid id)
        {
            return context.Policemen.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
