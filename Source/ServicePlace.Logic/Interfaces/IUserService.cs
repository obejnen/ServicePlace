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
        IdentityResult CreateUser(User user);

        IdentityResult UpdateUser(User user);

        IdentityResult DeleteUser(User user);

        User FindByEmail(string email);

        User FindByUserName(string username);

        User FindById(string id);

        ClaimsIdentity Authenticate(User user);

        IdentityResult CreateRole(Role role);
    }
}