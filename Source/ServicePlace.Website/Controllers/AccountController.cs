using System.Web.Mvc;
using Microsoft.Owin.Security;
using ServicePlace.Model;
using ServicePlace.Logic.Interfaces;
using System.Security.Claims;
using ServicePlace.Website.Models.AccountViewModels;
using Microsoft.AspNet.Identity;

namespace ServicePlace.Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        private readonly IAuthenticationManager _authenticationManager;

        public AccountController(IUserService userService, IAuthenticationManager authManager)
        {
            _userService = userService;
            _authenticationManager = authManager;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.UserName, Password = model.Password };
                ClaimsIdentity claim = _userService.Authenticate(user);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    _authenticationManager.SignOut();
                    _authenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    Password = model.Password,
                    Name = model.Name,
                    Role = "user"
                };
                var result = _userService.CreateUser(user);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                ModelState.AddModelError("Error", "Failed");
            }
            return View(model);
        }
    }
}