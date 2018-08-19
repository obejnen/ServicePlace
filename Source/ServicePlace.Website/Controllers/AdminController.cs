using System.Web.Mvc;
using ServicePlace.Common;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Logic.Interfaces.Services;

namespace ServicePlace.Website.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminMapper _adminMapper;
        private readonly IUserService _userService;

        public AdminController(IAdminMapper adminMapper, IUserService userService)
        {
            _adminMapper = adminMapper;
            _userService = userService;
        }
        public ActionResult Index()
        {
            var viewModel = _adminMapper.MapToIndexAdminViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeRole(string userId, string role)
        {
            if (role == Constants.AdminRoleName)
                _userService.AddToRole(userId, role);
            else
                _userService.RemoveFromRole(userId, Constants.AdminRoleName);
            return RedirectToAction("Index");
        }
    }
}