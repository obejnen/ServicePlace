using System.Web.Mvc;
using Microsoft.Owin.Security;
using ServicePlace.Logic.Interfaces.Services;
using ServicePlace.Model.ViewModels.AccountViewModels;
using Microsoft.AspNet.Identity;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Model.DTOModels;

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
        public new ActionResult Profile()
        {
            var viewModel = _userMapper.MapToProfileViewModel(_userService.FindByUserName(User.Identity.GetUserName()));
            return View("Profile", viewModel);
        }
    }
}