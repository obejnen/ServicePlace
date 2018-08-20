using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Logic.Interfaces.Services;
using Constants = ServicePlace.Common.Constants;

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

        public void GetLogs()
        {
            ViewBag.ElmahUrl = "/elmah.exd";
        }
    }
}