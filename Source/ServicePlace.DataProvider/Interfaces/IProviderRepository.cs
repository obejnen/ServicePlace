using System.Collections.Generic;
using ServicePlace.Model.LogicModels;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IProviderRepository : IRepository<Provider>
    {
        IEnumerable<Provider> Search(string search);

        IEnumerable<Provider> Take(int skip, int count);
    }
}