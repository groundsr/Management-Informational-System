using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.DataAccess.Abstractions
{
    public interface ISearchStrategy<T> where T:class
    {
        IEnumerable<T> Search(string term);
    }
}
