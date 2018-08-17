using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using ServicePlace.DataProvider.Managers;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;

        public IdentityRepository(UserManager userManager, RoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Create(User user)
        {
            _userManager.Create(user, user.PasswordHash);
        }

        public void Update(User user)
        {
            _userManager.Update(user);
        }

        public void Delete(User user)
        {
            _userManager.Delete(user);
        }

        public IEnumerable<User> GetAll() => _userManager.Users;

        public ClaimsIdentity Authenticate(string userName, string password)
        {
            var result = _userManager.Find(userName, password);
            return result == null
                ? null
                : _userManager.CreateIdentity(result, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public void AddToRole(string userId, string roleName)
        {
            _userManager.AddToRole(userId, roleName);
        }

        public void CreateRole(Role role)
        {
            var result = _roleManager.FindByName(role.Name);

            if (result != null) return;
            role.Id = Guid.NewGuid().ToString();
            _roleManager.Create(role);
        }

        public IQueryable<User> GetBy(Expression<Func<User, bool>> predicate)
        {
            return _userManager.Users.Where(predicate);
        }
    }
}