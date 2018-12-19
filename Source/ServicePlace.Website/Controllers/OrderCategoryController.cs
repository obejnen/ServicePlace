using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ServicePlace.Common;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Logic.Interfaces.Services;
using ServicePlace.Model.ViewModels.OrderCategoryViewModels;
using Constants = ServicePlace.Common.Constants;

namespace ServicePlace.Website.Controllers
{
    public class OrderCategoryController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderCategoryMapper _orderCategoryMapper;
        private readonly IOrderMapper _orderMapper;
        private readonly IUserService _userService;
        private readonly PageHelper _helper;

        public OrderCategoryController(IOrderService orderService,
            IOrderMapper orderMapper,
            IOrderCategoryMapper orderCategoryMapper,
            IUserService userService,
            PageHelper helper)
        {
            _orderService = orderService;
            _orderMapper = orderMapper;
            _orderCategoryMapper = orderCategoryMapper;
            _userService = userService;
            _helper = helper;
        }

        public ActionResult Create()
        {
            if (_userService.IsInRole(User.Identity.GetUserId(), Constants.AdminRoleName))
                return View("Admin/Create");
            return RedirectToAction("Index", "Order");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateOrderCategoryViewModel viewModel)
        {
            if (!ModelState.IsValid) return View("Admin/Create", viewModel);
            var category = _orderCategoryMapper.MapToOrderCategoryModel(viewModel);
            _orderService.CreateCategory(category);
            return RedirectToAction("Index", "Admin");

        }

        public ActionResult Index()
        {
            var viewModel = _orderCategoryMapper.MapToIndexOrderCategoryViewModel(_orderService.GetCategories());
            return View("_OrderCategoryIndex", viewModel);
        }

        public ActionResult Show(int id, int page = 1)
        {
            var orders = _orderService.GetByCategory(id).ToList();
            var pageRange = _helper.GetPageRange(page, _helper.GetPagesCount(orders.Count(), Constants.ItemsPerPage));
            ViewBag.CategoryId = id;
            return View("_OrderByCategoryIndex", _orderMapper.MapToIndexOrderViewModel(
                _orderService.GetPage(orders, page, Constants.ItemsPerPage),
                new[] { page, pageRange[0], pageRange[1] }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            _orderService.DeleteCategory(_orderService.GetCategory(id));
            var viewModel = _orderCategoryMapper.MapToIndexOrderCategoryViewModel(_orderService.GetCategories());
            return RedirectToAction("Index", "Admin");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var viewModel = _orderCategoryMapper.MapToCreateOrderCategoryViewModel(_orderService.GetCategory(id));
            return View("Admin/Edit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateOrderCategoryViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var orderCategory = _orderCategoryMapper.MapToOrderCategoryModel(viewModel);
            _orderService.UpdateCategory(orderCategory);
            return RedirectToAction("Index", "Admin");
        }
    }
}