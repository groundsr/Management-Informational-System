using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.DataAccess.Abstractions
{
    public interface IPolicemanRepository : IRepository<Policeman>
    {
        Policeman GetByUserId(Guid id);
        Policeman GetByEmail(string email);
        IEnumerable<Policeman> GetPolicemanByName(string name);

    }
}
