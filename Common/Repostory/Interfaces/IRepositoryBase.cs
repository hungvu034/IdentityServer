using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsApi.Common.Model;

namespace IsApi.Common.Repostory.Interfaces
{
    public interface IRepositoryBase<T, K>
    where T : EntityBase<K>
    {
        int SaveChanges();
        K Create(T entity);
        IEnumerable<T> FindAll();
        IEnumerable<T> FindByCondition(Func<T, bool> condition);
        T FindById(K id);
        T Update(T entity);
        void Delete(T entity);
    }
}