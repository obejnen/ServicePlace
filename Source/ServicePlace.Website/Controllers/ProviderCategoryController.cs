using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ServicePlace.Common;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Logic.Interfaces.Services;
using ServicePlace.Model.ViewModels.ProviderCategoryViewModels;
using Constants = ServicePlace.Common.Constants;

namespace ServicePlace.Website.Controllers
{
    public class ProviderCategoryController : Controller
    {
        private readonly IProviderService _providerService;
        private readonly IProviderMapper _providerMapper;
        private readonly IProviderCategoryMapper _providerCategoryMapper;
        private readonly IUserService _userService;
        private readonly PageHelper _helper;

        public ProviderCategoryController(IProviderService providerService,
            IProviderMapper providerMapper,
            IProviderCategoryMapper providerCategoryMapper,
            IUserService userService,
            PageHelper helper)
        {
            _providerService = providerService;
            _providerMapper = providerMapper;
            _providerCategoryMapper = providerCategoryMapper;
            _userService = userService;
            _helper = helper;
        }

        public ActionResult Index()
        {
            var viewModel =
                _providerCategoryMapper.MapToIndexProviderCategoryViewModel(_providerService.GetCategories());
            return View("_ProviderCategoryIndex", viewModel);
        }

        public ActionResult Create()
        {
            if (_userService.IsInRole(User.Identity.GetUserId(), Constants.AdminRoleName))
                return View("Admin/Create");
            return RedirectToAction("Index", "Provider");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateProviderCategoryViewModel viewModel)
        {
            var category = _providerCategoryMapper.MapToProviderCategoryModel(viewModel);
            _providerService.CreateCategory(category);
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult Show(int id, int page = 1)
        {
            var providers = _providerService.GetByCategory(id).ToList();
            var pageRange = _helper.GetPageRange(page, _helper.GetPagesCount(providers.Count, Constants.ItemsPerPage));
            ViewBag.HasPrice = true;
            ViewBag.Controller = "Provider";
            ViewBag.CategoryId = id;
            return View("_ProviderByCategoryIndex",
                _providerMapper
                    .MapToIndexProviderViewModel(_providerService.GetPage(providers, page, Constants.ItemsPerPage),
                        new[] { page, pageRange[0], pageRange[1] }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            _providerService.DeleteCategory(_providerService.GetCategory(id));
            var viewModel = _providerCategoryMapper.MapToIndexProviderCategoryViewModel(_providerService.GetCategories());
            return RedirectToAction("Index", "Admin");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var viewModel = _providerCategoryMapper.MapToCreateProviderCategoryViewModel(_providerService.GetCategory(id));
            return View("Admin/Edit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateProviderCategoryViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var orderCategory = _providerCategoryMapper.MapToProviderCategoryModel(viewModel);
            _providerService.UpdateCategory(orderCategory);
            return RedirectToAction("Index", "Admin");
        }
    }
}