using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Managers;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Repositories
{
    public class IdentityRepository : BaseRepository<User>, IIdentityRepository
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;

        protected override IEnumerable<Expression<Func<User, object>>> Includes =>
            new Expression<Func<User, object>>[]
            {
                x => x.Avatar,
                x => x.Profile
            };

        public IdentityRepository(UserManager userManager, RoleManager roleManager, ApplicationContext context) : base(context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public override void Create(User user)
        {
            _userManager.Create(user, user.PasswordHash);
        }

        public override void Update(User user)
        {
            _userManager.Update(user);
        }

        public override void Delete(User user)
        {
            _userManager.Delete(user);
        }

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
    }
}