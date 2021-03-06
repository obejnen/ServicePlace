﻿using System.Web.Mvc;
using Microsoft.Owin.Security;
using ServicePlace.Logic.Interfaces.Services;
using ServicePlace.Model.ViewModels.AccountViewModels;
using Microsoft.AspNet.Identity;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Model.DTOModels;
using Constants = ServicePlace.Common.Constants;

namespace ServicePlace.Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserMapper _userMapper;
        

        private readonly IAuthenticationManager _authenticationManager;

        public AccountController(IUserService userService,
            IUserMapper userMapper,
            IAuthenticationManager authManager)
        {
            _userService = userService;
            _userMapper = userMapper;
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
            if (!ModelState.IsValid) return View(model);
            var user = new UserDTO { UserName = model.UserName, Password = model.Password };
            var claim = _userService.Authenticate(user);
            if (claim == null)
                ModelState.AddModelError("", "Wrong login or password.");
            else
            {
                _authenticationManager.SignOut();
                _authenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = true
                }, claim);
                return RedirectToAction("Index", "Home");
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
            _userService.CreateRole(Constants.AdminRoleName);
            _userService.CreateRole(Constants.UserRoleName);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var userDto = _userMapper.MapToUserDtoModel(viewModel);
            _userService.Create(userDto);
            return Login(new LoginViewModel
            {
                UserName = userDto.UserName,
                Password = userDto.Password
            });
        }

        [Authorize]
        public ActionResult Profile(string id)
        {
            ProfileViewModel viewModel;
            if (!string.IsNullOrEmpty(id))
            {
                var user = _userService.Get(id);
                viewModel = _userMapper.MapToProfileViewModel(_userService.FindByUserName(user.UserName));
            }
            else
                viewModel = _userMapper.MapToProfileViewModel(_userService.FindByUserName(User.Identity.GetUserName()));
            return View("Profile", viewModel);
        }
    }
}