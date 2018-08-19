using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ServicePlace.Common;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Logic.Interfaces.Services;
using ServicePlace.Model.ViewModels.OrderViewModels;

namespace ServicePlace.Website.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IOrderMapper _orderMapper;
        private readonly PageHelper _helper;

        public OrderController(
            IOrderService orderService, 
            IUserService userService, 
            IOrderMapper orderMapper, 
            PageHelper helper)
        {
            _orderService = orderService;
            _userService = userService;
            _orderMapper = orderMapper;
            _helper = helper;
        }

        public ActionResult Index(int page = 1)
        {
            var pageRange = _helper.GetPageRange(page, _helper.GetPagesCount(_orderService.Orders.Count(), 8));
            var viewModel = _orderMapper.MapToIndexOrderViewModel(_orderService.GetPage(page, 8), new []{ page, pageRange[0], pageRange[1] });
            return View(viewModel);
        }

        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                var viewModel = _orderMapper.GetCreateOrderViewModel();
                return View(viewModel);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult Create(CreateOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = _orderMapper
                    .MapToOrderModel(model,
                                     _userService.FindByUserName(User.Identity.GetUserName()));

                _orderService.Create(order);
                return RedirectToAction("Show", "Order", new { id = _orderService.Orders.Last().Id });
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var viewModel = _orderMapper.MapToCreateOrderViewModel(_orderService.Get(id));
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(CreateOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = _orderMapper
                    .MapToOrderModel(model,
                        _userService.FindByUserName(User.Identity.GetUserName()));
                _orderService.Update(order);
                return RedirectToAction("Show", "Order", new { id = order.Id });
            }

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            _orderService.Delete(_orderService.Get(id));
            return RedirectToAction("Index", "Order");
        }

        [HttpPost]
        public ActionResult Close(int orderId)
        {
            if (User.Identity.GetUserId() == _orderService.Get(orderId).Creator.Profile.Id)
            {
                _orderService.CloseOrder(orderId);
            }
            return RedirectToAction("Show", "Order", new { id = orderId });
        }

        public ActionResult Show(int id)
        {
            var model = _orderMapper.MapToOrderViewModel(_orderService.Get(id));
            return View(model);
        }

        public ActionResult Search(string searchString, int page = 1)
        {
            var searchResult = _orderService.SearchOrder(searchString).ToList();
            var pageRange = _helper.GetPageRange(page, _helper.GetPagesCount(searchResult.Count(), 8));
            return View("Index",
                _orderMapper
                    .MapToIndexOrderViewModel(_orderService.GetPage(searchResult, page, 8),
                                              new []{ page, pageRange[0], pageRange[1]}));
        }
    }
}