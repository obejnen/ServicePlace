using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ServicePlace.Common;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Logic.Interfaces.Services;
using ServicePlace.Model.ViewModels.OrderViewModels;
using Constants = ServicePlace.Common.Constants;

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

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = _orderMapper.GetCreateOrderViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _orderMapper.GetCreateOrderViewModel().Categories;
                return View("Create", model);
            }
            var order = _orderMapper
                .MapToOrderModel(model,
                    _userService.FindByUserName(User.Identity.GetUserName()));

            _orderService.Create(order);
            return RedirectToAction("Show", "Order", new { id = _orderService.GetAll().Last().Id });

        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var viewModel = _orderMapper.MapToCreateOrderViewModel(_orderService.Get(id));
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateOrderViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var order = _orderMapper
                .MapToOrderModel(model,
                    _userService.FindByUserName(User.Identity.GetUserName()));
            _orderService.Update(order);
            return RedirectToAction("Show", "Order", new { id = order.Id });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            _orderService.Delete(_orderService.Get(id));
            return RedirectToAction("Index", "Order");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Close(int orderId)
        {
            if (User.Identity.GetUserId() == _orderService.Get(orderId).Creator.Profile.Id
                || User.IsInRole(Constants.AdminRoleName))
            {
                _orderService.CloseOrder(orderId);
            }
            return RedirectToAction("Show", "Order", new { id = orderId });
        }

        public ActionResult Show(int id)
        {
            var model = _orderMapper.MapToOrderViewModel(_orderService.Get(id));
            if(model.Approved || 
               User.IsInRole(Common.Constants.AdminRoleName) || 
               User.Identity.GetUserId() == model.User.Id) return View(model);
            return RedirectToAction("Index");
        }

        public ActionResult Search(string searchString, int page = 1, int categoryId = 0)
        {
            var searchResult = _orderService.SearchOrder(searchString, categoryId).ToList();
            var pageRange = _helper.GetPageRange(page, _helper.GetPagesCount(searchResult.Count(), 8));
            ViewBag.Action = "Search";
            return View("Index",
                _orderMapper
                    .MapToIndexOrderViewModel(_orderService.GetPage(searchResult, page, 8),
                                              new []{ page, pageRange[0], pageRange[1]}));
        }
    }
}