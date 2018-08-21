using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ServicePlace.Common;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Logic.Interfaces.Services;
using ServicePlace.Model.DataModels;
using ServicePlace.Model.ViewModels.AccountViewModels;
using ServicePlace.Model.ViewModels.OrderViewModels;

namespace ServicePlace.Logic.Mappers
{
    public class OrderMapper : IOrderMapper
    {
        private readonly IOrderService _orderService;
        private readonly IOrderCategoryMapper _orderCategoryMapper;

        public OrderMapper(IOrderService orderService, IOrderCategoryMapper orderCategoryMapper)
        {
            _orderService = orderService;
            _orderCategoryMapper = orderCategoryMapper;
        }

        public OrderViewModel MapToOrderViewModel(Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                Title = order.Title,
                Body = order.Body,
                Closed = order.Closed,
                Approved = order.Approved,
                Images = order.Images.Select(x => x.Url),
                CreatedAt = order.CreatedAt,
                User = new UserViewModel
                {
                    Id = order.Creator.Id,
                    Avatar = order.Creator.Avatar.Url,
                    Name = order.Creator.Profile.Name,
                    UserName = order.Creator.UserName
                }
            };
        }

        public IndexOrderViewModel MapToIndexOrderViewModel(IEnumerable<Order> orders, int[] pages)
        {
            var orderViewModels = orders.Select(MapToOrderViewModel).ToList();
            return new IndexOrderViewModel()
            {
                CurrentPage = pages[0],
                MinPage = pages[1],
                MaxPage = pages[2],
                FirstPart = orderViewModels.Count() > Constants.ItemsPerRow
                    ? orderViewModels.Take(Constants.ItemsPerRow)
                    : orderViewModels.Take(orderViewModels.Count()),
                SecondPart = orderViewModels.Count() > Constants.ItemsPerRow
                    ? orderViewModels.Skip(Constants.ItemsPerRow)
                    : null
            };
        }

        public IndexOrderViewModel MapToIndexOrderViewModel(IEnumerable<Order> orders)
        {
            return new IndexOrderViewModel()
            {
                Orders = orders.Select(MapToOrderViewModel)
            };
        }

        public IEnumerable<SelectListItem> MapToSelectListItems(IEnumerable<Order> orders)
        {
            return orders.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Title
            });
        }

        public CreateOrderViewModel GetCreateOrderViewModel()
        {
            return new CreateOrderViewModel
            {
                Categories = _orderCategoryMapper.MapToSelectListItems(_orderService.GetCategories())
            };
        }

        public CreateOrderViewModel MapToCreateOrderViewModel(Order order)
        {
            return new CreateOrderViewModel
            {
                Id = order.Id,
                Title = order.Title,
                Body = order.Body,
                Categories = _orderCategoryMapper.MapToSelectListItems(_orderService.GetCategories()),
                CategoryId = order.Category.Id,
                CreatedAt = order.CreatedAt
            };
        }

        public Order MapToOrderModel(CreateOrderViewModel createOrderViewModel, User creator)
        {
            return new Order
            {
                Id = createOrderViewModel.Id,
                Title = createOrderViewModel.Title,
                Body = createOrderViewModel.Body,
                Category = _orderService.GetCategory(createOrderViewModel.CategoryId),
                Creator = creator,
                Images = createOrderViewModel.Images?.Trim().Split(' ').Select(image => new Image {Url = image}).ToList()
            };
        }
    }
}
