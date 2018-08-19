using System.Web.Mvc;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Logic.Interfaces.Services;

namespace ServicePlace.Website.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminMapper _adminMapper;

        public AdminController(IAdminMapper adminMapper)
        {
            _adminMapper = adminMapper;
        }
        public ActionResult Index()
        {
            var viewModel = _adminMapper.MapToIndexAdminViewModel();
            return View(viewModel);
        }
    }
}