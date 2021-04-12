using MSI.DataAccess;
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
        protected readonly PoliceContext _context;
        private DbSet<T> dbSet;
        public EFRepository(PoliceContext context)
        {
            this._context = context;
            dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
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
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
