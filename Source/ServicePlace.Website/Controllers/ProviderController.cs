using System.Collections.Generic;
using AutoMapper;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ServicePlace.Logic.Interfaces;
using ServicePlace.Model.LogicModels;
using ServicePlace.Model.ViewModels.AccountViewModels;
using ServicePlace.Website.Models.ProviderViewModels;

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

        public ActionResult Index()
        {
            var model = Mapper.Map<IEnumerable<IndexViewModel>>(_providerService.Providers);
            return View(model);
        }

        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
                return View();
            return RedirectToAction("Index", "Provider");
        }



        [HttpPost]
        public RedirectToRouteResult Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var executor = new Provider
                {
                    Title = model.Title,
                    Body = model.Body,
                    Price = model.Price,
                    Creator = _userService.FindById(User.Identity.GetUserId())
                };

                _providerService.Create(executor);
            }

            return RedirectToAction("Index", "Provider");
        }

        public ActionResult Show(int id)
        {
            var provider = _providerService.FindById(id);
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
    }
}