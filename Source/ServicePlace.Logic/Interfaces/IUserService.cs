using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using ServicePlace.Model;

namespace ServicePlace.Logic.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<IdentityResult> CreateUserAsync(User user);

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<IdentityResult> DeleteUserAsync(User user);

        Task<User> FindByEmailAsync(string email);

        Task<User> FindByUserNameAsync(string username);

        Task<User> FindByIdAsync(string id);

        Task<ClaimsIdentity> AuthenticateAsync(User user);

        Task<IdentityResult> CreateRoleAsync(Role role);

        Task SetInitialData(User admin, IEnumerable<string> roles);
    }
}