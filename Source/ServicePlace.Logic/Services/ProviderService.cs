using System;
using System.Collections.Generic;
using System.Linq;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Logic.Interfaces;
using ServicePlace.Model.LogicModels;

namespace ServicePlace.Logic.Services
{
    public class ProviderService : Interfaces.IProviderService
    {
        private readonly IProviderRepository _providerRepository;

        public ProviderService(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public IEnumerable<Provider> Providers => _providerRepository.GetAll();

        public void Create(Provider provider)
        {
            provider.CreatedAt = DateTime.Now;
            _providerRepository.Create(provider);
        }

        public void Delete(Provider provider)
        {
            _providerRepository.Delete(provider);
        }

        public void Update(Provider provider)
        {
            _providerRepository.Update(provider);
        }

        public Provider FindById(object id)
        {
            return _providerRepository.FindById(id);
        }

        public IEnumerable<Provider> Search(string search)
        {
            return _providerRepository.Search(search);
        }

        public IEnumerable<Provider> Take(int skip, int count)
        {
            return _providerRepository.Take(skip, count);
        }

        public IEnumerable<Provider> GetUserProviders(User user)
        {
            return _providerRepository.GetAll().Where(x => user.Id == x.Creator.Id);
        }
    }
}