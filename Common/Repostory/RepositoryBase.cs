using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsApi.Common.Model;
using IsApi.Common.Repostory.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IsApi.Common.Repostory
{
    public class RepositoryBase<T, K, TContext> : IRepositoryBase<T, K>
    where T : EntityBase<K>
    where TContext : DbContext
    {
        protected readonly TContext _context ;

        public RepositoryBase(TContext context)
        {
            _context = context;
        }

        public K Create(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
            return entity.Id ; 
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<T> FindAll()
        {
            return _context.Set<T>();
        }

        public IEnumerable<T> FindByCondition(Func<T, bool> condition)
        {
            return FindAll().Where(condition);
        }

        public T FindById(K id)
        {
            return FindAll().Where(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public T Update(T entity)
        {
            var currentValue = FindById(entity.Id);
            _context.Entry(currentValue).CurrentValues.SetValues(entity);
            return entity; 
        }
    }
}