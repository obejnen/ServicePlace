using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Web.Mvc;
using System.Web.WebPages;
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
            var model = Mapper.Map<List<ItemViewModel>>(_orderService.GetPage(page, 8));
            var viewModel = new IndexViewModel
            {
                FirstPart = model.Count() > 4 ? model.Take(4) : model.Take(model.Count()),
                SecondPart = model.Count > 4 ? model.Skip(4) : null
            };
            return View(viewModel);
        }

        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = new CreateViewModel
                {
                    Categories = _orderService.GetCategories().Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    })
                };
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public RedirectToRouteResult Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var imageList = new List<Image>();
                if(model.Images != null)
                    foreach (var image in model.Images.Trim().Split(' '))
                    {
                        imageList.Add(new Image
                        {
                            Url = image
                        });
                    }
                var order = new Order
                {
                    Title = model.Title,
                    Body = model.Body,
                    Closed = false,
                    Category = _orderService.FindCategoryById(model.CategoryId),
                    Creator = _userService.FindById(User.Identity.GetUserId()),
                    Images = imageList
                };

                _orderService.Create(order);
            }

            return RedirectToAction("Index", "Order");
        }

        [HttpPost]
        public ActionResult Close(int orderId)
        {
            if (User.Identity.GetUserId() == _orderService.FindById(orderId).Creator.Id)
            {
                _orderService.Close(orderId);
            }
            return RedirectToAction("Show", "Order", new { id = orderId });
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
                Closed = order.Closed,
                Images = order.Images,
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