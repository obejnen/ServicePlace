﻿using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Logic.Interfaces.Services;
using ServicePlace.Model.ViewModels.ProviderResponseViewModels;

namespace ServicePlace.Website.Controllers
{
    public class ProviderResponseController : Controller
    {
        private readonly IProviderService _providerService;
        private readonly IUserService _userService;
        private readonly IProviderResponseMapper _providerResponseMapper;

        public ProviderResponseController(
            IProviderService providerService,
            IUserService userSerivce,
            IProviderResponseMapper providerReseponseMapper)
        {
            _providerService = providerService;
            _userService = userSerivce;
            _providerResponseMapper = providerReseponseMapper;
        }

        public ActionResult Create(int providerId)
        {
            var viewModel =
                _providerResponseMapper
                    .GetCreateProviderResponseViewModel(User.Identity.GetUserId(), providerId);
            return View("_ProviderResponsePartial", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateProviderResponseViewModel viewModel)
        {
            var providerResponse = _providerResponseMapper
                .MapToProviderResponseModel(viewModel, _userService.FindByUserName(User.Identity.GetUserName()));
            _providerService.CreateResponse(providerResponse);
            var responseModel = _providerResponseMapper.MapToProviderResponseViewModel(providerResponse);
            return PartialView("Partials/_ProviderResponse", responseModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public int Delete(int id)
        {
            var responseToDelete = _providerService.GetAllProviderResponses().SingleOrDefault(x => x.Id == id);
            _providerService.DeleteResponse(responseToDelete);
            return id;
        }

        public ActionResult Index(int providerId)
        {
            var providerResponses = _providerResponseMapper
                .MapToIndexProviderResponseViewModel(_providerService
                                                        .GetProviderResponses(providerId));
            return View("_ProviderResponseIndexPartial", providerResponses);
        }
    }
}