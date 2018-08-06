using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T model);

        void Delete(T model);

        void Update(T model);

        T FindById(object id);

        IEnumerable<T> GetAll();

        void Dispose();
    }
}