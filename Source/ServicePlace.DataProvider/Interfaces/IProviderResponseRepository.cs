using System.Collections.Generic;
using ServicePlace.Model.LogicModels;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IProviderResponseRepository : IRepository<ProviderResponse>
    {
        IEnumerable<ProviderResponse> GetProviderResponses(int providerId);
    }
}
