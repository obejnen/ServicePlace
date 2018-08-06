using System.Security.Claims;
using Microsoft.AspNet.Identity;
using ServicePlace.Model;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IIdentityRepository : IRepository<User>
    {
        User FindByEmail(string email);

        User FindByUserName(string username);

        ClaimsIdentity Authenticate(User user);

        IdentityResult CreateRole(Role role);
    }
}