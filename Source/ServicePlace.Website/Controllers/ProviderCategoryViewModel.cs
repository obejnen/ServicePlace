using System.Linq;
using System.Web.Mvc;
using ServicePlace.Common;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Logic.Interfaces.Services;

namespace ServicePlace.Website.Controllers
{
    public class ProviderCategoryController : Controller
    {
        private readonly IProviderService _providerService;
        private readonly IProviderMapper _providerMapper;
        private readonly IProviderCategoryMapper _providerCategoryMapper;
        private readonly PageHelper _helper;

        public ProviderCategoryController(IProviderService providerService,
            IProviderMapper providerMapper,
            IProviderCategoryMapper providerCategoryMapper,
            PageHelper helper)
        {
            _providerService = providerService;
            _providerMapper = providerMapper;
            _providerCategoryMapper = providerCategoryMapper;
            _helper = helper;
        }

        public ActionResult Index()
        {
            var viewModel =
                _providerCategoryMapper.MapToIndexProviderCategoryViewModel(_providerService.GetCategories());
            return View("_ProviderCategoryIndex", viewModel);
        }

        public ActionResult Show(int id, int page = 1)
        {
            var providers = _providerService.GetByCategory(id).ToList();
            var pageRange = _helper.GetPageRange(page, _helper.GetPagesCount(providers.Count(), 8));
            return RedirectToAction("Index", "Order",
                _providerMapper
                    .MapToIndexProviderViewModel(_providerService.GetPage(providers, page, 8),
                        new[] { page, pageRange[0], pageRange[1] }));
        }
    }
}