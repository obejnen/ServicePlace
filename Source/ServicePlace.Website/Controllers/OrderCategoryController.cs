using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using ServicePlace.Common;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Logic.Interfaces.Services;
using ServicePlace.Model.ViewModels.OrderCategoryViewModels;
using ServicePlace.Model.ViewModels.OrderViewModels;

namespace ServicePlace.Website.Controllers
{
    public class OrderCategoryController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderMapper _orderMapper;
        private readonly PageHelper _helper;

        public OrderCategoryController(IOrderService orderService, IOrderMapper orderMapper, PageHelper helper)
        {
            _orderService = orderService;
            _orderMapper = orderMapper;
            _helper = helper;
        }

        public ActionResult Index()
        {
            var list = new List<ItemViewModel>();
            foreach (var model in _orderService.GetCategories())
            {
                list.Add(new ItemViewModel
                {
                    Id = model.Id,
                    Name = model.Name
                });
            }
            return View("_OrderCategoryIndex", list);
        }

        public ActionResult Show(int id, int page = 1)
        {
            var orders = _orderService.GetByCategory(id).ToList();
            var pageRange = _helper.GetPageRange(page, _helper.GetPagesCount(orders.Count(), 8));
            return RedirectToAction("Index", "Order",
                _orderMapper
                    .MapToIndexOrderViewModel(_orderService.GetPage(orders, page, 8),
                        new[] { page, pageRange[0], pageRange[1] }));
        }
    }
}