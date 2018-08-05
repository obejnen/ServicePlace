using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ServicePlace.DataProvider.Mappers;
using ServicePlace.DataProvider.Managers;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Interfaces;
using CommonModels = ServicePlace.Model;
using DataModels = ServicePlace.DataProvider.Entities;

namespace ServicePlace.DataProvider.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly ApplicationContext _context;
        private readonly IProfileManager _profileManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;

        private bool _disposed;

        public IdentityRepository()
        {
            _context = new ApplicationContext();
            _userManager = new UserManager(new UserStore<DataModels.User>(_context));
            _roleManager = new RoleManager(new RoleStore<DataModels.Role>(_context));
            _profileManager = new ProfileManager();
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

            _userManager.AddToRoleAsync(model.Id, user.Role);
            _profileManager.CreateAsync(user, model.Id);
            SaveChangesAsync();
            return Task.FromResult(IdentityResult.Success);
        }

        public async Task<IdentityResult> UpdateUserAsync(CommonModels.User user)
        {
            var model = new UserMapper().MapToDataModel(user);
            var result = await _userManager.UpdateAsync(model);

            if (result.Errors.Any())
            {
                return IdentityResult.Failed(result.Errors.ToArray());
            }

            _profileManager.UpdateAsync(user);
            await SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteUserAsync(CommonModels.User user)
        {
            var model = new UserMapper().MapToDataModel(user);
            var result = await _userManager.DeleteAsync(model);

            if (result.Errors.Any())
            {
                return IdentityResult.Failed(result.Errors.ToArray());
            }

            _profileManager.DeleteAsync(user);
            await SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<CommonModels.User> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user == null
                ? null 
                : new UserMapper().MapToCommonModel(user);
        }

        public async Task<CommonModels.User> FindByUserNameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return new UserMapper().MapToCommonModel(user);
        }

        public async Task<CommonModels.User> FindByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return new UserMapper().MapToCommonModel(user);
        }

        public Task<ClaimsIdentity> AuthenticateAsync(CommonModels.User user)
        {
            var result = _userManager.FindAsync(user.Email, user.Password).Result;
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
                _roleManager.CreateAsync(model);
                return Task.FromResult(IdentityResult.Success);
            }

            return Task.FromResult(IdentityResult.Failed($"Cannot create role with name {model.Name}"));
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
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