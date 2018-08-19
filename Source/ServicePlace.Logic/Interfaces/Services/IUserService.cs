using System.Collections.Generic;
using System.Security.Claims;
using ServicePlace.Model.DataModels;
using ServicePlace.Model.DTOModels;

namespace ServicePlace.Logic.Interfaces.Services
{
    public interface IUserService : IService<UserDTO>
    {
        User FindByEmail(string email);

        User FindByUserName(string username);

        ClaimsIdentity Authenticate(UserDTO user);

        bool IsInRole(string userId, string roleName);

        void CreateRole(string roleName);

        void AddToRole(string userId, string roleName);

        void RemoveFromRole(string userId, string roleName);

        IEnumerable<User> GetAll();
    }
}