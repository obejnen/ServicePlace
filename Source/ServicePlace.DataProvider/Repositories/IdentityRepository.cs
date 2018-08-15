using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ServicePlace.DataProvider.Mappers;
using ServicePlace.DataProvider.Managers;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Model.LogicModels;

namespace ServicePlace.DataProvider.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IProfileManager _profileManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly UserMapper _mapper;
        private readonly log4net.ILog _log;

        private bool _disposed;

        public IdentityRepository(UserManager userManager, RoleManager roleManager, IProfileManager profileManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _profileManager = profileManager;
            _mapper = new UserMapper();
            _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public void Create(User user)
        {
            var model = _mapper.MapToDataModel(user);
            model.Id = Guid.NewGuid().ToString();
            _userManager.Create(model, user.Password);
            _log.Info($"Created user {model.UserName}");
            _userManager.AddToRole(model.Id, user.Role);
            _log.Info($"User {model.UserName} added to role {user.Role}");
            _profileManager.Create(user, model.Id);
            _log.Info($"Profile for user {model.UserName} created");
        }

        public void Update(User user)
        {
            var model = _mapper.MapToDataModel(user);
            _userManager.Update(model);
            _profileManager.Update(user);
            _log.Info($"User {model.UserName} and profile updated");
        }

        public void Delete(User user)
        {
            var model = _mapper.MapToDataModel(user);
            _userManager.Delete(model);
            _profileManager.Delete(user);
            _log.Info($"User ${model.UserName} and profile removed");
        }

        public User FindByEmail(string email)
        {
            var user = _userManager.FindByEmail(email);
            return user == null
                ? null 
                : _mapper.MapToCommonModel(user);
        }

        public User FindByUserName(string username)
        {
            var user = _userManager.FindByName(username);
            return _mapper.MapToCommonModel(user);
        }

        public User FindById(object id)
        {
            var user = _userManager.FindByIdAsync((string)id).Result;
            return _mapper.MapToCommonModel(user);
        }

        public ClaimsIdentity Authenticate(User user)
        {
            var result = _userManager.Find(user.UserName, user.Password);
            return result == null
                ? null
                : _userManager.CreateIdentity(result, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public IdentityResult CreateRole(Role role)
        {

            var model = new RoleMapper().MapToDataModel(role);
            var result = _roleManager.FindByName(model.Name);

            if (result == null)
            {
                model.Id = Guid.NewGuid().ToString();
                _roleManager.Create(model);
                return IdentityResult.Success;
            }

            return IdentityResult.Failed($"Cannot create role with name {model.Name}");
        }

        public IEnumerable<User> GetAll() => _userManager.Users.ToList().Select(x => _mapper.MapToCommonModel(x));

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _userManager.Dispose();
                _roleManager.Dispose();
                _profileManager.Dispose();
            }
            _disposed = true;
        }

    }
}