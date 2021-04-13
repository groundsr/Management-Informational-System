using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.DataAccess.Abstractions
{
    public interface IEFPolicemanRepository:IRepository<Policeman>
    {
         Policeman GetPolicemanByEmail(string name);

    }
}
