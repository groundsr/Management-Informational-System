using MSI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.DataAccess.Abstractions
{
    public interface ISearchStrategyUsingId<T> : ISearchStrategy<T> where T:class
    {
        IEnumerable<T> SearchUsingId(string term, Guid id=new Guid());
    }
}
