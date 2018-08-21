using System.Security.Claims;
using Microsoft.AspNet.Identity;
using ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IIdentityRepository : IRepository<User>
    {
        ClaimsIdentity Authenticate(string userName, string password);

        bool IsInRole(string userId, string roleName);

        void CreateRole(Role role);

        void AddToRole(string userId, string roleName);

        void RemoveFromRole(string userId, string roleName);
    }
}