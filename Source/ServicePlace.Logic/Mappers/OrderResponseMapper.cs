using System.Collections.Generic;
using System.Linq;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Logic.Interfaces.Services;
using ServicePlace.Model.DataModels;
using ServicePlace.Model.ViewModels.OrderResponseViewModels;

namespace ServicePlace.Logic.Mappers
{
    public class OrderResponseMapper : IOrderResponseMapper
    {
        private readonly IOrderMapper _orderMapper;
        private readonly IProviderMapper _providerMapper;
        private readonly IProviderService _providerService;
        private readonly IOrderService _orderService;

        public OrderResponseMapper(IOrderMapper orderMapper, 
            IProviderMapper providerMapper, 
            IProviderService providerService,
            IOrderService orderService)
        {
            _orderMapper = orderMapper;
            _providerMapper = providerMapper;
            _providerService = providerService;
            _orderService = orderService;
        }

        public OrderResponseViewModel MapToOrderResponseViewModel(OrderResponse orderResponse)
        {
            return new OrderResponseViewModel
            {
                Id = orderResponse.Id,
                Price = orderResponse.Price,
                Comment = orderResponse.Comment,
                Completed = orderResponse.Completed,
                CreatedAt = orderResponse.CreatedAt,
                Order = _orderMapper.MapToOrderViewModel(orderResponse.Order),
                Provider = _providerMapper.MapToProviderViewModel(orderResponse.Provider)
            };
        }

        public CreateOrderResponseViewModel GetCreateOrderResponseViewModel(string userId, int orderId)
        {
            return new CreateOrderResponseViewModel
            {
                OrderId = orderId,
                Providers = _providerMapper.MapToSelectListItems(_providerService.GetUserProviders(userId))
            };
        }

        public IndexOrderResponseViewModel MapToIndexOrderResponseViewModel(IEnumerable<OrderResponse> orderResponses)
        {
            return new IndexOrderResponseViewModel
            {
                OrderResponses = orderResponses.Select(MapToOrderResponseViewModel)
            };
        }

        public OrderResponse MapToOrderResponseModel(CreateOrderResponseViewModel createViewModel, User creator)
        {
            return new OrderResponse
            {
                Comment = createViewModel.Comment,
                Price = createViewModel.Price,
                Order = _orderService.Get(createViewModel.OrderId),
                Provider = _providerService.Get(createViewModel.ProviderId),
                Creator = creator
            };
        }
    }
}
