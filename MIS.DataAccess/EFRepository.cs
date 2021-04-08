using API.DataAccess;
using Microsoft.EntityFrameworkCore;
using MIS.DataAccess.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MIS.DataAccess
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly PoliceContext context;
        private DbSet<T> dbSet;
        public EFRepository(PoliceContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }

        public T Get(Guid id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        public void Remove(Guid id)
        {
            dbSet.Remove(Get(id));
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
            context.SaveChanges();
        }
    }
}
