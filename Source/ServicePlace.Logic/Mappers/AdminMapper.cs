using System.Linq;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Logic.Interfaces.Services;
using ServicePlace.Model.ViewModels.AdminViewModels;

namespace ServicePlace.Logic.Mappers
{
    public class AdminMapper : IAdminMapper
    {
        private readonly IOrderService _orderService;
        private readonly IProviderService _providerService;
        private readonly IOrderMapper _orderMapper;
        private readonly IProviderMapper _providerMapper;
        private readonly IOrderResponseMapper _orderResponseMapper;
        private readonly IProviderResponseMapper _providerResponseMapper;
        private readonly IUserMapper _userMapper;
        private readonly IUserService _userService;

        public AdminMapper(IOrderService orderService,
            IProviderService providerService,
            IUserService userService,
            IOrderMapper orderMapper,
            IProviderMapper providerMapper,
            IOrderResponseMapper orderResponseMapper,
            IUserMapper userMapper,
            IProviderResponseMapper providerResponseMapper)
        {
            _orderService = orderService;
            _providerService = providerService;
            _userService = userService;
            _orderMapper = orderMapper;
            _providerMapper = providerMapper;
            _orderResponseMapper = orderResponseMapper;
            _providerResponseMapper = providerResponseMapper;
            _userMapper = userMapper;
        }

        public IndexAdminViewModel MapToIndexAdminViewModel()
        {
            return new IndexAdminViewModel
            {
                Users = _userService.GetAll().Select(x => _userMapper.MapToUserDtoModel(x)),
                Orders = _orderMapper.MapToIndexOrderViewModel(_orderService.GetAll()),
                OrderResponses = _orderResponseMapper.MapToIndexOrderResponseViewModel(_orderService.GetAllOrderResponses()),
                Providers = _providerMapper.MapToIndexProviderViewModel(_providerService.GetAll()),
                ProviderResponses = _providerResponseMapper.MapToIndexProviderResponseViewModel(_providerService.GetAllProviderResponses())
            };
        }
    }
}