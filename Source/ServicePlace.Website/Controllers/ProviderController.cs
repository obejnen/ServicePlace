using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ServicePlace.Common;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Logic.Interfaces.Services;
using ServicePlace.Model.ViewModels.ProviderViewModels;
using Constants = ServicePlace.Common.Constants;

namespace ServicePlace.Website.Controllers
{
    public class ProviderController : Controller
    {
        private readonly IProviderService _providerService;
        private readonly IUserService _userService;
        private readonly IProviderMapper _providerMapper;
        private readonly PageHelper _helper;

        public ProviderController(IProviderService providerService,
            IUserService userService, 
            IProviderMapper providerMapper,
            PageHelper helper)
        {
            _providerService = providerService;
            _userService = userService;
            _providerMapper = providerMapper;
            _helper = helper;
        }

        public ActionResult Index(int page = 1)
        {
            var pageRange = _helper.GetPageRange(page, _helper.GetPagesCount(_providerService.Providers.Count(), 8));
            var viewModel = _providerMapper
                .MapToIndexProviderViewModel(_providerService.GetPage(page, 8), new[] { page, pageRange[0], pageRange[1] });

            return View(viewModel);
        }

        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                var viewModel = _providerMapper.GetCreateProviderViewModel();

                return View(viewModel);
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateProviderViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var creator = _userService.FindByUserName(User.Identity.GetUserName());
            var provider = _providerMapper.MapToProviderModel(model, creator);

            _providerService.Create(provider);

            return RedirectToAction("Show", new { id = _providerService.Providers.Last().Id });
        }

        public ActionResult Edit(int id)
        {
            var viewModel = _providerMapper.MapToCreateProviderViewModel(_providerService.Get(id));
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateProviderViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var provider = _providerMapper
                .MapToProviderModel(model,
                    _userService.FindByUserName(User.Identity.GetUserName()));
            _providerService.Update(provider);
            return RedirectToAction("Show", new { id = provider.Id });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            _providerService.Delete(_providerService.Get(id));
            return RedirectToAction("Index");
        }

        public ActionResult Show(int id)
        {
            var viewModel = _providerMapper.MapToProviderViewModel(_providerService.Get(id));

            return View(viewModel);
        }

        public ActionResult Search(string searchString, int page = 1)
        {
            var searchResult = _providerService.SearchProvider(searchString).ToList();
            var pageRange = _helper.GetPageRange(page, _helper.GetPagesCount(searchResult.Count(), Constants.ItemsPerPage));

            return View("Index",
                _providerMapper
                    .MapToIndexProviderViewModel(_providerService.GetPage(searchResult, page, Constants.ItemsPerPage),
                        new[] { page, pageRange[0], pageRange[1] }));
        }
    }
}