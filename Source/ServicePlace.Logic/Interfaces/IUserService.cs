using System;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using ServicePlace.Model.LogicModels;

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