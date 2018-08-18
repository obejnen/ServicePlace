using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Logic.Interfaces.Services;
using ServicePlace.Model.DataModels;
using ServicePlace.Model.DTOModels;
using ServicePlace.Model.ViewModels;
using ServicePlace.Model.ViewModels.AccountViewModels;

namespace ServicePlace.Logic.Mappers
{
    public class UserMapper : IUserMapper
    {
        private readonly IOrderService _orderService;
        private readonly IProviderService _providerService;
        private readonly IOrderMapper _orderMapper;
        private readonly IProviderMapper _providerMapper;
        private readonly IOrderResponseMapper _orderResponseMapper;
        private readonly IProviderResponseMapper _providerResponseMapper;

        public UserMapper(IOrderService orderService,
            IProviderService providerService,
            IOrderMapper orderMapper,
            IProviderMapper providerMapper,
            IOrderResponseMapper orderResponseMapper,
            IProviderResponseMapper providerResponseMapper)
        {
            _orderService = orderService;
            _providerService = providerService;
            _orderMapper = orderMapper;
            _providerMapper = providerMapper;
            _orderResponseMapper = orderResponseMapper;
            _providerResponseMapper = providerResponseMapper;
        }

        public ProfileViewModel MapToProfileViewModel(User user)
        {
            var orders = _orderMapper.MapToIndexOrderViewModel(_orderService.GetUserOrders(user.Id));
            var providers = _providerMapper.MapToIndexProviderViewModel(_providerService.GetUserProviders(user.Id));
            var orderResponses = _orderResponseMapper.MapToIndexOrderResponseViewModel(_orderService.GetUserResponses(user.Id));
            var providerResponses = _providerResponseMapper.MapToIndexProviderResponseViewModel(_providerService.GetUserResponses(user.Id));
            var profile = new ProfileViewModel();
            profile.Id = user.Id;
            profile.Avatar = user.Avatar.Url;
            profile.UserName = user.UserName;
            profile.Name = user.Profile.Name;
            profile.Orders = orders;
            profile.Providers = providers;
            profile.OrderResponses = orderResponses;
            profile.ProviderResponses = providerResponses;
            return profile;
        }

        public UserDTO MapToUserDtoModel(RegisterViewModel registerViewModel)
        {
            return new UserDTO
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.UserName,
                Password = registerViewModel.Password,
                Name = registerViewModel.Name,
                Avatar = registerViewModel.Avatar.Trim()
            };
        }
    }
}
