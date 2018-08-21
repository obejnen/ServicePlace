using System.Collections.Generic;
using ServicePlace.Model.DataModels;

namespace ServicePlace.Logic.Interfaces.Services
{
    public interface IProviderService : IService<Provider>
    {
        IEnumerable<Provider> Providers { get; }

        IEnumerable<Provider> SearchProvider(string query);

        void ApproveProvider(int providerId);

        IEnumerable<Provider> Take(int skip, int count);

        IEnumerable<Provider> GetPage(int page, int perPage);

        IEnumerable<Provider> GetPage(IEnumerable<Provider> providers, int page, int perPage);

        IEnumerable<Provider> GetUserProviders(string userId);

        void CreateResponse(ProviderResponse response);

        void DeleteResponse(ProviderResponse response);

        IEnumerable<ProviderResponse> GetProviderResponses(int providerId);

        IEnumerable<ProviderResponse> GetUserResponses(string userId);

        IEnumerable<Provider> GetByCategory(int categoryId);

        ProviderCategory GetCategory(int categoryId);

        IEnumerable<ProviderCategory> GetCategories();

        IEnumerable<Provider> GetAll();

        IEnumerable<ProviderResponse> GetAllProviderResponses();

        void CreateCategory(ProviderCategory providerCategory);
    }
}