using System;
using System.Collections.Generic;
using System.Linq;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Logic.Interfaces;
using ServicePlace.Model.DataModels;

namespace ServicePlace.Logic.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IProviderResponseRepository _responseRepository;

        public ProviderService(
            IProviderRepository providerRepository,
            IProviderResponseRepository providerResponseRepository)
        {
            _providerRepository = providerRepository;
            _responseRepository = providerResponseRepository;
        }

        public IEnumerable<Provider> Providers => _providerRepository.GetAll();

        public void Create(Provider provider)
        {
            provider.CreatedAt = DateTime.Now;
            provider.UpdatedAt = provider.CreatedAt;
            _providerRepository.Create(provider);
        }

        public void Delete(Provider provider)
        {
            _providerRepository.Delete(provider);
        }

        public void Update(Provider provider)
        {
            provider.UpdatedAt = DateTime.Now;
            _providerRepository.Update(provider);
        }

        public Provider Get(object id)
        {
            return _providerRepository.GetBy(x => x.Id == (int) id).SingleOrDefault();
        }

        public IEnumerable<Provider> SearchProvider(string search)
        {
            return _providerRepository.GetBy(x => x.Title.Contains(search) || x.Body.Contains(search));
        }

        public IEnumerable<Provider> Take(int skip, int count)
        {
            return _providerRepository.Take(skip, count);
        }

        public IEnumerable<Provider> GetUserProviders(string userId)
        {
            return _providerRepository.GetBy(x => x.Creator.Id == userId);
        }

        public IEnumerable<Provider> GetPage(int page, int perPage)
        {
            int providersCount = _providerRepository.GetAll().Count();
            int skip = (page - 1) * perPage;
            return skip + perPage > providersCount
                ? Take((page - 1) * perPage, providersCount % perPage)
                : Take((page - 1) * perPage, perPage);
        }

        public int GetPagesCount(int perPage)
        {
            var providersCount = _providerRepository.GetAll().Count();
            int count = providersCount / perPage;
            return count * perPage == providersCount ? count : count + 1;
        }

        public void CreateResponse(ProviderResponse response)
        {
            response.CreatedAt = DateTime.Now;
            _responseRepository.Create(response);
        }

        public IEnumerable<ProviderResponse> GetProviderResponses(int providerId)
        {
            return _responseRepository.GetBy(x => x.Provider.Id == providerId);
        }

        public IEnumerable<ProviderResponse> GetUserResponses(string userId)
        {
            return _responseRepository.GetBy(x => x.Creator.Id == userId);
        }
    }
}