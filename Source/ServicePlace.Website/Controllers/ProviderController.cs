using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ServicePlace.Common;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Logic.Interfaces.Services;
using ServicePlace.Model.ViewModels.ProviderViewModels;

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
        public ActionResult Create(CreateProviderViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var creator = _userService.FindByUserName(User.Identity.GetUserName());
            var provider = _providerMapper.MapToProviderModel(model, creator);

            _providerService.Create(provider);

            return Show(_providerService.Providers.Last().Id);
        }

        public ActionResult Show(int id)
        {
            var viewModel = _providerMapper.MapToProviderViewModel(_providerService.Get(id));

            return View(viewModel);
        }

        public ActionResult Search(string searchString, int page = 1)
        {
            var searchResult = _providerService.SearchProvider(searchString).ToList();
            var pageRange = _helper.GetPageRange(page, _helper.GetPagesCount(searchResult.Count(), 8));

            return View("Index",
                _providerMapper
                    .MapToIndexProviderViewModel(_providerService.GetPage(searchResult, page, 8),
                        new[] { page, pageRange[0], pageRange[1] }));
        }
    }
}