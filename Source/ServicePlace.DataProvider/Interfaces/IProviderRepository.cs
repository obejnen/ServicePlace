using System.Collections.Generic;
using ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IProviderRepository : IRepository<Provider>
    {
        IEnumerable<Provider> Take(int skip, int count);
    }
}