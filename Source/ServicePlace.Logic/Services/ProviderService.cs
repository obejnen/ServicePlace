using System;
using System.Collections.Generic;
using System.Linq;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Logic.Interfaces;
using ServicePlace.Model.LogicModels;

namespace ServicePlace.Logic.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProviderResponseRepository _responseRepository;

        public ProviderService(
            IProviderRepository providerRepository,
            IProviderResponseRepository providerResponseRepository,
            IOrderRepository orderRepository)
        {
            _providerRepository = providerRepository;
            _responseRepository = providerResponseRepository;
            _orderRepository = orderRepository;
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

        public IEnumerable<Provider> GetPage(int page, int perPage)
        {
            int providersCount = _providerRepository.GetProvidersCount();
            int skip = (page - 1) * perPage;
            return skip + perPage > providersCount
                ? Take((page - 1) * perPage, providersCount % perPage)
                : Take((page - 1) * perPage, perPage);
        }

        public int GetPagesCount(int perPage)
        {
            var providersCount = _providerRepository.GetProvidersCount();
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
            return _responseRepository.GetProviderResponses(providerId);
        }

        public IEnumerable<Order> GetProviderResponse(int providerId, IEnumerable<int> ordersId)
        {
            List<Order> responses = new List<Order>();

            foreach (var orderId in ordersId)
            {
                responses.Add(_orderRepository.GetOrderProvider(providerId, orderId));
            }

            return responses;
        }

        public IEnumerable<Provider> GetUserProviders(string userId)
        {
            return _providerRepository.GetUserProviders(userId);
        }

        public IEnumerable<ProviderResponse> GetUserResponses(string userId)
        {
            return _responseRepository.GetUserResponses(userId);
        }
    }
}