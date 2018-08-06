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
        IdentityResult CreateUser(CommonModels.User user);

        IdentityResult UpdateUser(CommonModels.User user);

        IdentityResult DeleteUser(CommonModels.User user);

        CommonModels.User FindByEmail(string email);

        CommonModels.User FindByUserName(string username);

        CommonModels.User FindById(string id);

        ClaimsIdentity Authenticate(CommonModels.User user);

        IdentityResult CreateRole(CommonModels.Role role);
    }
}