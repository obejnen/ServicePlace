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
        void Create(User user);

        void Update(User user);

        void Delete(User user);

        User FindByEmail(string email);

        User FindByUserName(string username);

        User FindById(object id);

        ClaimsIdentity Authenticate(User user);

        IdentityResult CreateRole(Role role);
    }
}