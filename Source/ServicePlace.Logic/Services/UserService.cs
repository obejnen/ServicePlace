using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using CommonModels = ServicePlace.Model;
using DataModels = ServicePlace.DataProvider.Entities;
using ServicePlace.Logic.Interfaces;
using ServicePlace.DataProvider.Interfaces;

namespace ServicePlace.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IIdentityRepository _repository;

        public UserService(IIdentityRepository repository)
        {
            _repository = repository;
        }

        public async Task<IdentityResult> CreateUserAsync(CommonModels.User user) =>
            await _repository.FindByEmailAsync(user.Email) == null
                ? await _repository.CreateUserAsync(user)
                : IdentityResult.Failed($"User with email {user.Email} already exists");

        public async Task<IdentityResult> UpdateUserAsync(CommonModels.User user) =>
            await _repository.FindByIdAsync(user.Id) == null
                ? IdentityResult.Failed("Cannot find user")
                : await _repository.UpdateUserAsync(user);

        public async Task<IdentityResult> DeleteUserAsync(CommonModels.User user) =>
            await _repository.FindByIdAsync(user.Id) == null
                ? IdentityResult.Failed("Cannot find user")
                : await _repository.DeleteUserAsync(user);

        public async Task<CommonModels.User> FindByIdAsync(string id) =>
            await _repository.FindByIdAsync(id);

        public async Task<CommonModels.User> FindByEmailAsync(string email) =>
            await _repository.FindByEmailAsync(email);

        public async Task<CommonModels.User> FindByUserNameAsync(string username) =>
            await _repository.FindByUserNameAsync(username);

        public Task<ClaimsIdentity> AuthenticateAsync(CommonModels.User user) => _repository.AuthenticateAsync(user);

        public async Task SetInitialData(CommonModels.User user, IEnumerable<string> roles)
        {
            foreach (var roleName in roles)
            {
                await _repository.CreateRoleAsync(new CommonModels.Role { Name = roleName });
            }

            await CreateUserAsync(user);
        }

        public Task<IdentityResult> CreateRoleAsync(CommonModels.Role role)
        {
            return _repository.CreateRoleAsync(role);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}