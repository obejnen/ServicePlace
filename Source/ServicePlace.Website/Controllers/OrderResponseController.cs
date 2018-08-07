using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ServicePlace.Logic.Interfaces;
using ServicePlace.Model.LogicModels;
using ServicePlace.Model.ViewModels.OrderResponseViewModels;
using ServicePlace.Model.ViewModels.ProviderViewModels;

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

        public ActionResult Create()
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
                Providers = providerViewModels
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateViewModel model)
        {
            var orderResponse = new OrderResponse
            {
                Order = _orderService.FindById(model.OrderId),
                Provider = _providerService.FindById(model.ProviderId),
                Price = model.Price
            };
            return View(model);
        }
    }
}