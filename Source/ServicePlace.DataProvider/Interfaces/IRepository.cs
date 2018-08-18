using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T model);

        void Delete(T model);

        void Update(T model);

        void SaveChanges();

        IEnumerable<T> GetAll();

        IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);
    }
}