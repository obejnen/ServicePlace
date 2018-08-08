using System.Collections.Generic;
using AutoMapper;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ServicePlace.Logic.Interfaces;
using ServicePlace.Model.LogicModels;
using ServicePlace.Model.ViewModels.AccountViewModels;
using ServicePlace.Model.ViewModels.OrderViewModels;

namespace ServicePlace.Website.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public OrderController(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        public ActionResult Index()
        {
            var model = Mapper.Map<IEnumerable<IndexViewModel>>(_orderService.Orders);
            return View(model);
        }

        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
                return View();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public RedirectToRouteResult Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    Title = model.Title,
                    Body = model.Body,
                    Creator = _userService.FindById(User.Identity.GetUserId())
                };

                _orderService.Create(order);
            }

            return RedirectToAction("Index", "Order");
        }

        public ActionResult Show(int id)
        {
            var order = _orderService.FindById(id);
            var creator = _userService.FindById(order.Creator.Id);
            var creatorViewModel = new CreatorViewModel
            {
                Id = creator.Id,
                Name = creator.Name,
                UserName = creator.UserName
            };
            var viewModel = new ShowViewModel
            {
                Id = order.Id,
                Title = order.Title,
                Body = order.Body,
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt,
                Creator = creatorViewModel
            };

            return View(viewModel);
        }
    }
}