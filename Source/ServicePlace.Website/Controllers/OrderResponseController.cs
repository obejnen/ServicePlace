using System.Collections.Generic;
using System.Web.Mvc;
using ServicePlace.Logic.Interfaces;
using ServicePlace.Model.LogicModels;
using ServicePlace.Model.ViewModels.AccountViewModels;
using ServicePlace.Model.ViewModels.OrderResponseViewModels;
using CreateViewModel = ServicePlace.Model.ViewModels.OrderResponseViewModels.CreateViewModel;

namespace ServicePlace.Website.Controllers
{
    public class OrderResponseController : Controller
    {
        private readonly IProviderService _providerService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public OrderResponseController(IProviderService providerService, IUserService userService, IOrderService orderService)
        {
            _providerService = providerService;
            _orderService = orderService;
            _userService = userService;
        }

        public ActionResult Create(int orderId)
        {
            var providerViewModels = new List<SelectListItem>();
            var providers = _providerService.GetUserProviders(_userService.FindByUserName(User.Identity.Name));
            foreach (var provider in providers)
            {
                providerViewModels.Add(new SelectListItem
                {
                    Value = provider.Id.ToString(),
                    Text = provider.Title
                });
            }

            var model = new CreateViewModel
            {
                OrderId = orderId,
                Providers = providerViewModels
            };
            return View("_OrderResponsePartial", model);
        }

        [HttpPost]
        public ActionResult Create(CreateViewModel model)
        {
            var orderResponse = new OrderResponse
            {
                Order = _orderService.FindById(model.OrderId),
                Provider = _providerService.FindById(model.ProviderId),
                Creator = _userService.FindByUserName(User.Identity.Name),
                Completed = false,
                Price = model.Price,
                Comment = model.Comment
            };
            _orderService.CreateResponse(orderResponse);
            return RedirectToAction("Show", "Order", new { id = model.OrderId });
        }

        [HttpPost]
        public ActionResult Complete(int orderId, int orderResponseId)
        {
            _orderService.Complete(orderId, orderResponseId);
            return RedirectToAction("Show", "Order", new {id = orderId});
        }

        public ActionResult Index(int orderId)
        {
            var orderResponses = _orderService.GetOrderResponses(orderId);
            var list = new List<IndexViewModel>();
            var order = _orderService.FindById(orderId);
            foreach (var orderResponse in orderResponses)
            {
                list.Add(new IndexViewModel
                {
                    Id = orderResponse.Id,
                    Order = new Model.ViewModels.OrderViewModels.ItemViewModel
                    {
                        Id = order.Id,
                        Closed = order.Closed,
                        Creator = new CreatorViewModel
                        {
                            Id = order.Creator.Id
                        }
                    },
                    Provider = new Model.ViewModels.ProviderViewModels.IndexViewModel
                    {
                        Id = orderResponse.Provider.Id,
                        Title = orderResponse.Provider.Title,
                    },
                    Price = orderResponse.Price,
                    Comment = orderResponse.Comment,
                    CreatedAt = orderResponse.CreatedAt,
                    Completed = orderResponse.Completed
                });
            }

            return View("_OrderResponseIndexPartial", list);
        }
    }
}