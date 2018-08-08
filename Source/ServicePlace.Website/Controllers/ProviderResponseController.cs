using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ServicePlace.Logic.Interfaces;
using ServicePlace.Model.ViewModels.ProviderResponseViewModels;

namespace ServicePlace.Website.Controllers
{
    public class ProviderResponseController : Controller
    {
        private readonly IProviderService _providerService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public ProviderResponseController(
            IProviderService providerService,
            IOrderService orderService,
            IUserService userSerivce)
        {
            _providerService = providerService;
            _orderService = orderService;
            _userService = userSerivce;
        }

        public ActionResult Create(int providerId)
        {
            var ordersListModel = new List<SelectListItem>();
            var user = _userService.FindByUserName(User.Identity.Name);
            var orders = _orderService.Orders.Where(x => x.Creator.Id == user.Id).ToList();

            foreach (var order in orders)
            {
                var item = _orderService.GetOrderProvider(providerId, order.Id);
                if (item == null)
                    break;
                ordersListModel.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Title
                });
            }

            var model = new CreateViewModel
            {
                ProviderId = providerId,
                Orders = ordersListModel
            };
            return View("_ProviderResponsePartial", model);
        }
    }
}