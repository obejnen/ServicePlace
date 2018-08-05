using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using ServicePlace.DataProvider.Mappers;
using ServicePlace.DataProvider.Managers;
using ServicePlace.DataProvider.Interfaces;
using CommonModels = ServicePlace.Model;

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

        public Task<IdentityResult> CreateUserAsync(CommonModels.User user)
        {
            var model = new UserMapper().MapToDataModel(user);
            model.Id = Guid.NewGuid().ToString();
            var result = _userManager.CreateAsync(model, user.Password).Result;
            if (result.Errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(result.Errors.ToArray()));
            }

            _userManager.AddToRole(model.Id, user.Role);
            _profileManager.CreateAsync(user, model.Id);
            return Task.FromResult(IdentityResult.Success);
        }

        public async Task<IdentityResult> UpdateUserAsync(CommonModels.User user)
        {
            var model = _mapper.MapToDataModel(user);
            var result = await _userManager.UpdateAsync(model);

            if (result.Errors.Any())
            {
                return IdentityResult.Failed(result.Errors.ToArray());
            }

            _profileManager.UpdateAsync(user);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteUserAsync(CommonModels.User user)
        {
            var model = _mapper.MapToDataModel(user);
            var result = await _userManager.DeleteAsync(model);

            if (result.Errors.Any())
            {
                return IdentityResult.Failed(result.Errors.ToArray());
            }

            _profileManager.DeleteAsync(user);
            return IdentityResult.Success;
        }

        public async Task<CommonModels.User> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user == null
                ? null 
                : _mapper.MapToCommonModel(user);
        }

        public async Task<CommonModels.User> FindByUserNameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return _mapper.MapToCommonModel(user);
        }

        public async Task<CommonModels.User> FindByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return _mapper.MapToCommonModel(user);
        }

        public Task<ClaimsIdentity> AuthenticateAsync(CommonModels.User user)
        {
            var result = _userManager.Find(user.UserName, user.Password);
            return result == null
                ? null
                : _userManager.CreateIdentityAsync(result, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public Task<IdentityResult> CreateRoleAsync(CommonModels.Role role)
        {
            var model = new RoleMapper().MapToDataModel(role);
            var result = _roleManager.FindByNameAsync(model.Name).Result;

            if (result == null)
            {
                model.Id = Guid.NewGuid().ToString();
                _roleManager.Create(model);
                return Task.FromResult(IdentityResult.Success);
            }

            return Task.FromResult(IdentityResult.Failed($"Cannot create role with name {model.Name}"));
        }

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