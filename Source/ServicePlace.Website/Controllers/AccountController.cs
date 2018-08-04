using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using ServicePlace.Model;
using ServicePlace.Logic.Interfaces;
using ServicePlace.Logic.Services;
using System.Security.Claims;
using ServicePlace.Common.Enums;
using ServicePlace.Website.Models.AccountViewModels;
using Microsoft.AspNet.Identity;

namespace ServicePlace.Website.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService => HttpContext.GetOwinContext().GetUserManager<IUserService>();

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

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
                User user = new User { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = UserService.AuthenticateAsync(user).Result;
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
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
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }


        public async Task<ActionResult> Register()
        {
            await SetInitialDataAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
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
                var result = await UserService.CreateUserAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                ModelState.AddModelError("Error", "Failed");
            }
            return View(model);
        }
        private async Task SetInitialDataAsync()
        {
            await UserService.CreateRoleAsync(new ServicePlace.Model.Role { Name = "user" });
            //await UserService.SetInitialData(new User
            //{
            //    Email = "somemail@mail.ru",
            //    UserName = "somemail@mail.ru",
            //    Password = "ad46D_ewr3",
            //    Name = "Семен Семенович Горбунков",
            //    Role = "admin",
            //}, new List<string> { "user", "admin" });
        }
    }
}