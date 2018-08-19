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
            var pageRange = _helper.GetPageRange(page, _helper.GetPagesCount(orders.Count(), 8));
            ViewBag.CategoryId = id;
            return View("_OrderByCategoryIndex", _orderMapper.MapToIndexOrderViewModel(
                _orderService.GetPage(orders, page, 8),
                new[] { page, pageRange[0], pageRange[1] }));
        }
    }
}