using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using ServicePlace.Model.LogicModels;
using ServicePlace.Logic.Interfaces;
using System.Security.Claims;
using ServicePlace.Model.ViewModels.AccountViewModels;
using Microsoft.AspNet.Identity;
using ServicePlace.Model.ViewModels;
using ServicePlace.Model.ViewModels.OrderViewModels;

namespace ServicePlace.Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IProviderService _providerService;

        private readonly IAuthenticationManager _authenticationManager;

        public AccountController(IUserService userService
            , IOrderService orderService
            , IProviderService providerService
            , IAuthenticationManager authManager)
        {
            _userService = userService;
            _orderService = orderService;
            _providerService = providerService;
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
                _userService.Create(user);
                return Login(new LoginViewModel
                {
                    UserName = user.UserName,
                    Password = user.Password
                });
            }
            return View(model);
        }

        public ActionResult Profile()
        {
            var user = _userService.FindByUserName(User.Identity.Name);
            var orders = _orderService.GetUserOrders(user.Id);
            var providers = _providerService.GetUserProviders(user.Id);
            var orderResponses = _orderService.GetUserResponses(user.Id);
            var providerResponses = _providerService.GetUserResponses(user.Id);

            var orderViewModels = new List<ItemViewModel>();

            foreach (var order in orders)               //move to mapper
            {
                orderViewModels.Add(new ItemViewModel
                {
                    Id = order.Id,
                    Body = order.Body,
                    Title = order.Title
                });
            }

            var providerViewModels = new List<Model.ViewModels.ProviderViewModels.IndexViewModel>();

            foreach (var provider in providers) //move to mapper
            {
                providerViewModels.Add(new Model.ViewModels.ProviderViewModels.IndexViewModel
                {
                    Id = provider.Id,
                    Body = provider.Body,
                    Title = provider.Title
                });
            }

            var orderResponseViewModels = new List<Model.ViewModels.OrderResponseViewModels.IndexViewModel>();

            foreach (var orderResponse in orderResponses)
            {
                orderResponseViewModels.Add(new Model.ViewModels.OrderResponseViewModels.IndexViewModel
                {
                    Order = new ItemViewModel
                    {
                        Id = orderResponse.Order.Id,
                        Title = orderResponse.Order.Title
                    }
                });
            }

            var providerResponseViewModels = new List<Model.ViewModels.ProviderResponseViewModels.IndexViewModel>();

            foreach (var providerResponse in providerResponses)
            {
                providerResponseViewModels.Add(new Model.ViewModels.ProviderResponseViewModels.IndexViewModel
                {
                    Provider = new Model.ViewModels.ProviderViewModels.IndexViewModel
                    {
                        Id = providerResponse.Provider.Id,
                        Title = providerResponse.Provider.Title
                    },
                    Order = new ItemViewModel
                    {
                        Id = providerResponse.Order.Id,
                        Title = providerResponse.Order.Title
                    }
                });
            }

            ProfileViewModel model = new ProfileViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                Orders = orderViewModels,
                Providers = providerViewModels,
                OrderResponses = orderResponseViewModels,
                ProviderResponses = providerResponseViewModels
            };

            return View("Profile", model);
        }
    }
}