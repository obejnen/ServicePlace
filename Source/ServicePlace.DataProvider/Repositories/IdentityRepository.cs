using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using ServicePlace.DataProvider.Mappers;
using ServicePlace.DataProvider.Managers;
using ServicePlace.DataProvider.Interfaces;
using CommonModels = ServicePlace.Model.LogicModels;

namespace ServicePlace.DataProvider.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IProfileManager _profileManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly UserMapper _mapper;

        private bool _disposed;

        public IdentityRepository(UserManager userManager, RoleManager roleManager, IProfileManager profileManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _profileManager = profileManager;
            _mapper = new UserMapper();
        }

        public void Create(CommonModels.User user)
        {
            var model = new UserMapper().MapToDataModel(user);
            model.Id = Guid.NewGuid().ToString();
            _userManager.Create(model, user.Password);
            _userManager.AddToRole(model.Id, user.Role);
            _profileManager.Create(user, model.Id);
        }

        public void Update(CommonModels.User user)
        {
            var model = _mapper.MapToDataModel(user);
            _userManager.Update(model);
            _profileManager.Update(user);
        }

        public void Delete(CommonModels.User user)
        {
            var model = _mapper.MapToDataModel(user);
            _userManager.Delete(model);
            _profileManager.Delete(user);
        }

        public CommonModels.User FindByEmail(string email)
        {
            var user = _userManager.FindByEmail(email);
            return user == null
                ? null 
                : _mapper.MapToCommonModel(user);
        }

        public CommonModels.User FindByUserName(string username)
        {
            var user = _userManager.FindByName(username);
            return _mapper.MapToCommonModel(user);
        }

        public CommonModels.User FindById(object id)
        {
            var user = _userManager.FindById((string)id);
            return _mapper.MapToCommonModel(user);
        }

        public ClaimsIdentity Authenticate(CommonModels.User user)
        {
            var result = _userManager.Find(user.UserName, user.Password);
            return result == null
                ? null
                : _userManager.CreateIdentity(result, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public IdentityResult CreateRole(CommonModels.Role role)
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

        public IEnumerable<CommonModels.User> GetAll() => _userManager.Users.ToList().Select(x => _mapper.MapToCommonModel(x));

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