using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MIS.DataAccess.Abstractions
{
    public interface IRepository<T> where T : class
    {
        T Get(Guid id);
        void Add(T entity);
        void Remove(Guid id);
        void Update(T entity);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null);

        void Save();
    }
}
