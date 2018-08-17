using System.Collections.Generic;
using AutoMapper;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ServicePlace.Common;
using ServicePlace.Logic.Interfaces;
using ServicePlace.Model.LogicModels;
using ServicePlace.Model.ViewModels.AccountViewModels;
using ServicePlace.Model.ViewModels.ProviderViewModels;

namespace ServicePlace.Website.Controllers
{
    public class ProviderController : Controller
    {
        private readonly IProviderService _providerService;
        private readonly IUserService _userService;

        public ProviderController(IProviderService providerService, IUserService userService)
        {
            _providerService = providerService;
            _userService = userService;
        }

        public ActionResult Index(int page = 1)
        {
            var helper = new PageHelper();
            ViewBag.CurrentPage = page;
            ViewBag.PageRange = helper.GetPageRange(page, _providerService.GetPagesCount(8));
            var model = Mapper.Map<IEnumerable<IndexViewModel>>(_providerService.GetPage(page, 8));
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
                var provider = new Provider
                {
                    Title = model.Title,
                    Body = model.Body,
                    Price = model.Price,
                    Creator = _userService.FindById(User.Identity.GetUserId())
                };

                _providerService.CreateProvider(provider);
            }

            return RedirectToAction("Index", "Provider");
        }

        public ActionResult Show(int id)
        {
            var provider = _providerService.GetProvider(id);
            var creator = _userService.FindById(provider.Creator.Id);
            var creatorViewModel = new CreatorViewModel
            {
                Id = creator.Id,
                Name = creator.Name,
                UserName = creator.UserName
            };
            var viewModel = new ShowViewModel
            {
                Id = provider.Id,
                Title = provider.Title,
                Body = provider.Body,
                Price = provider.Price,
                CreatedAt = provider.CreatedAt,
                UpdatedAt = provider.UpdatedAt,
                Creator = creatorViewModel
            };

            return View(viewModel);
        }

        public ActionResult Search(string searchString)
        {
            return View("Index", Mapper.Map<IEnumerable<IndexViewModel>>(_providerService.SearchProvider(searchString)));
        }
    }
}