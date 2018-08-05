using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using ServicePlace.DataProvider.Managers;
using CommonModels = ServicePlace.Model;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IIdentityRepository : IDisposable
    {
        Task<IdentityResult> CreateUserAsync(CommonModels.User user);

        Task<IdentityResult> UpdateUserAsync(CommonModels.User user);

        Task<IdentityResult> DeleteUserAsync(CommonModels.User user);

        Task<CommonModels.User> FindByEmailAsync(string email);

        Task<CommonModels.User> FindByUserNameAsync(string username);

        Task<CommonModels.User> FindByIdAsync(string id);

        Task<ClaimsIdentity> AuthenticateAsync(CommonModels.User user);

        Task<IdentityResult> CreateRoleAsync(CommonModels.Role role);
    }
}