using System.Collections.Generic;
using AutoMapper;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ServicePlace.Logic.Interfaces;
using ServicePlace.Model.LogicModels;
using ServicePlace.Model.ViewModels.AccountViewModels;
using ServicePlace.Website.Models.ExecutorViewModels;

namespace ServicePlace.Website.Controllers
{
    public class ExecutorController : Controller
    {
        private readonly IExecutorService _executorService;
        private readonly IUserService _userService;

        public ExecutorController(IExecutorService executorService, IUserService userService)
        {
            _executorService = executorService;
            _userService = userService;
        }

        public ActionResult Index()
        {
            var model = Mapper.Map<IEnumerable<IndexViewModel>>(_executorService.Executors);
            return View(model);
        }

        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
                return View();
            return RedirectToAction("Index", "Executor");
        }



        [HttpPost]
        public RedirectToRouteResult Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var executor = new Executor
                {
                    Title = model.Title,
                    Body = model.Body,
                    Price = model.Price,
                    Creator = _userService.FindById(User.Identity.GetUserId())
                };

                _executorService.Create(executor);
            }

            return RedirectToAction("Index", "Executor");
        }

        public ActionResult Show(int id)
        {
            var executor = _executorService.FindById(id);
            var creator = _userService.FindById(executor.Creator.Id);
            var creatorViewModel = new CreatorViewModel
            {
                Id = creator.Id,
                Name = creator.Name,
                UserName = creator.UserName
            };
            var viewModel = new ShowViewModel
            {
                Id = executor.Id,
                Title = executor.Title,
                Body = executor.Body,
                Price = executor.Price,
                CreatedAt = executor.CreatedAt,
                UpdatedAt = executor.UpdatedAt,
                Creator = creatorViewModel
            };

            return View(viewModel);
        }
    }
}