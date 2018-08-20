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
        private readonly IOrderService _orderService;
        private readonly IProviderService _providerService;

        public AdminController(IAdminMapper adminMapper,
            IUserService userService, 
            IOrderService orderService,
            IProviderService providerService)
        {
            _adminMapper = adminMapper;
            _userService = userService;
            _orderService = orderService;
            _providerService = providerService;
        }

        public ActionResult Index()
        {
            var viewModel = _adminMapper.MapToIndexAdminViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveOrder(int orderId)
        {
            _orderService.ApproveOrder(orderId);
            return RedirectToAction("Show", "Order", new { id = orderId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveProvider(int providerId)
        {
            _providerService.ApproveProvider(providerId);
            return RedirectToAction("Show", "Provider", new { id = providerId });
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