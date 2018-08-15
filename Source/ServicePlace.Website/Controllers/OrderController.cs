using System.Collections.Generic;
using AutoMapper;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ServicePlace.Common;
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

        public ActionResult Index(int page = 1)
        {
            _userService.CreateRole(new Role {Name = "user"});
            var helper = new PageHelper();
            ViewBag.CurrentPage = page;
            ViewBag.PageRange = helper.GetPageRange(page, _orderService.GetPagesCount(8));
            var model = Mapper.Map<IEnumerable<IndexViewModel>>(_orderService.GetPage(page, 8));
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

        public ActionResult Search(string searchString)
        {
            return View("Index", Mapper.Map<IEnumerable<IndexViewModel>>(_orderService.Search(searchString)));
        }
    }
}