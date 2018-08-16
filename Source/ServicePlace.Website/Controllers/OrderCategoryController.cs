using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AutoMapper;
using ServicePlace.Logic.Interfaces;
using ServicePlace.Model.ViewModels.OrderCategoryViewModels;
using ServicePlace.Model.ViewModels.OrderResponseViewModels;

namespace ServicePlace.Website.Controllers
{
    public class OrderCategoryController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderCategoryController(IOrderService orderService)
        {
            _orderService = orderService;
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

        public ActionResult Show(int id)
        {
            var model = new List<Model.ViewModels.OrderViewModels.ItemViewModel>();
            foreach (var order in _orderService.GetByCategory(id))
            {
                model.Add(new Model.ViewModels.OrderViewModels.ItemViewModel
                {
                    Id = order.Id,
                    CreatedAt = order.CreatedAt,
                    Body = order.Body,
                    Title = order.Title
                });
            }
            //var model = Mapper.Map<List<Model.ViewModels.OrderViewModels.ItemViewModel>>(_orderService.GetByCategory(id));
            var viewModel = new Model.ViewModels.OrderViewModels.IndexViewModel
            {
                FirstPart = model.Count() > 4 ? model.Take(4) : model.Take(model.Count()),
                SecondPart = model.Count > 4 ? model.Skip(4) : null
            };
            return RedirectToAction("Index", "Order", viewModel);
        }
    }
}