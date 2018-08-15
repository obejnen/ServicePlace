using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ServicePlace.Logic.Interfaces;
using ServicePlace.Model.LogicModels;
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
                if (item != null)
                {
                    ordersListModel.Add(new SelectListItem
                    {
                        Value = item.Id.ToString(),
                        Text = item.Title
                    });
                }
            }

            var model = new CreateViewModel
            {
                ProviderId = providerId,
                Orders = ordersListModel
            };
            return View("_ProviderResponsePartial", model);
        }

        [HttpPost]
        public ActionResult Create(CreateViewModel model)
        {
            var providerResponse = new ProviderResponse
            {
                Order = _orderService.FindById(model.OrderId),
                Provider = _providerService.FindById(model.ProviderId),
                Creator = _userService.FindByUserName(User.Identity.Name),
                Comment = model.Comment
            };

            _providerService.CreateResponse(providerResponse);
            return RedirectToAction("Index", $"Provider/{model.ProviderId}");
        }

        public ActionResult Index(int providerId)
        {
            var providerResponses = _providerService.GetProviderResponses(providerId);
            var list = new List<IndexViewModel>();
            foreach (var orderResponse in providerResponses)
            {
                list.Add(new IndexViewModel
                {
                    Order = new Model.ViewModels.OrderViewModels.ItemViewModel
                    {
                        Id = orderResponse.Order.Id,
                        Title = orderResponse.Order.Title,
                    },
                    Comment = orderResponse.Comment,
                    CreatedAt = orderResponse.CreatedAt
                });
            }

            return View("_ProviderResponseIndexPartial", list);
        }
    }
}