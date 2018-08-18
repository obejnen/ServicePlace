using System.Collections.Generic;
using System.Linq;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Logic.Interfaces.Services;
using ServicePlace.Model.DataModels;
using ServicePlace.Model.ViewModels.ProviderResponseViewModels;

namespace ServicePlace.Logic.Mappers
{
    public class ProviderResponseMapper : IProviderResponseMapper
    {
        private readonly IProviderMapper _providerMapper;
        private readonly IOrderMapper _orderMapper;
        private readonly IOrderService _orderService;
        private readonly IProviderService _providerService;

        public ProviderResponseMapper(IProviderMapper providerMapper,
            IOrderMapper orderMapper,
            IOrderService orderService,
            IProviderService providerService)
        {
            _providerMapper = providerMapper;
            _orderMapper = orderMapper;
            _orderService = orderService;
            _providerService = providerService;
        }

        public ProviderResponseViewModel MapToProviderResponseViewModel(ProviderResponse providerResponse)
        {
            return new ProviderResponseViewModel
            {
                Order = _orderMapper.MapToOrderViewModel(providerResponse.Order),
                Provider = _providerMapper.MapToProviderViewModel(providerResponse.Provider),
                Comment = providerResponse.Comment,
                CreatedAt = providerResponse.CreatedAt,
            };
        }

        public IndexProviderResponseViewModel MapToIndexProviderResponseViewModel(
            IEnumerable<ProviderResponse> providerResponses)
        {
            return new IndexProviderResponseViewModel
            {
                ProviderResponses = providerResponses.Select(MapToProviderResponseViewModel)
            };
        }

        public CreateProviderResponseViewModel GetCreateProviderResponseViewModel(string userId, int providerId)
        {
            var orders = _orderService.GetProvidedOrders(userId, providerId);
            return new CreateProviderResponseViewModel
            {
                ProviderId = providerId,
                Orders = _orderMapper.MapToSelectListItems(orders)
            };
        }

        public ProviderResponse MapToProviderResponseModel(CreateProviderResponseViewModel createViewModel, User creator)
        {
            return new ProviderResponse
            {
                Comment = createViewModel.Comment,
                Creator = creator,
                Order = _orderService.Get(createViewModel.OrderId),
                Provider = _providerService.Get(createViewModel.ProviderId)
            };
        }
    }
}