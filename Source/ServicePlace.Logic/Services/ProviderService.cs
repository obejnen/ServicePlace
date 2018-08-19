using System;
using System.Collections.Generic;
using System.Linq;
using ServicePlace.Common;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Logic.Interfaces.Services;
using ServicePlace.Model.DataModels;

namespace ServicePlace.Logic.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IProviderResponseRepository _responseRepository;
        private readonly IProviderCategoryRepository _categoryRepository;
        private readonly IContextProvider _contextProvider;

        public ProviderService(
            IProviderRepository providerRepository,
            IProviderResponseRepository providerResponseRepository,
            IProviderCategoryRepository categoryRepository,
            IContextProvider contextProvider)
        {
            _providerRepository = providerRepository;
            _responseRepository = providerResponseRepository;
            _categoryRepository = categoryRepository;
            _contextProvider = contextProvider;
        }

        public IEnumerable<Provider> Providers => _providerRepository.GetAll();

        public void Create(Provider provider)
        {
            provider.CreatedAt = DateTime.Now;
            if (provider.Images == null)
                provider.Images = new[] { new Image { Url = Constants.DefaultOrderImage } };
            _providerRepository.Create(provider);
            _contextProvider.CommitChanges();
        }

        public void Delete(Provider provider)
        {
            _providerRepository.Delete(provider);
            _contextProvider.CommitChanges();
        }

        public void Update(Provider provider)
        {
            provider.CreatedAt = _providerRepository.GetBy(x => x.Id == provider.Id).SingleOrDefault().CreatedAt;
            _providerRepository.Update(provider);
            _contextProvider.CommitChanges();
        }

        public Provider Get(object id)
        {
            return _providerRepository.GetBy(x => x.Id == (int) id).SingleOrDefault();
        }

        public IEnumerable<Provider> GetAll() => _providerRepository.GetAll();

        public IEnumerable<ProviderResponse> GetAllProviderResponses() => _responseRepository.GetAll();

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
            var providersCount = _providerRepository.GetAll().Count();
            var skip = (page - 1) * perPage;
            return skip + perPage > providersCount
                ? Take(skip, providersCount % perPage)
                : Take(skip, perPage);
        }

        public IEnumerable<Provider> GetPage(IEnumerable<Provider> providers, int page, int perPage)
        {
            var providerList = providers.ToList();
            var providersCount = providerList.Count;
            var skip = (page - 1) * perPage;
            return skip + perPage > providersCount
                ? providerList.Skip(skip).Take(providersCount % perPage)
                : providerList.Skip(skip).Take(perPage);
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
            _contextProvider.CommitChanges();
        }

        public IEnumerable<ProviderResponse> GetProviderResponses(int providerId) =>
            _responseRepository.GetBy(x => x.Provider.Id == providerId);

        public ProviderCategory GetCategory(int categoryId) =>
            _categoryRepository.GetBy(x => x.Id == categoryId).SingleOrDefault();

        public IEnumerable<ProviderResponse> GetUserResponses(string userId) =>
            _responseRepository.GetBy(x => x.Creator.Id == userId);

        public IEnumerable<Provider> GetByCategory(int categoryId) =>
            _providerRepository.GetBy(x => x.Category.Id == categoryId);

        public IEnumerable<ProviderCategory> GetCategories() => _categoryRepository.GetAll();
    }
}