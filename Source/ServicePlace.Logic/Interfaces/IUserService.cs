using System.Security.Claims;
using ServicePlace.Model.DataModels;
using ServicePlace.Model.DTOModels;

namespace ServicePlace.Logic.Interfaces
{
    public interface IUserService : IService<UserDTO>
    {
        UserDTO FindByEmail(string email);

        UserDTO FindByUserName(string username);

        ClaimsIdentity Authenticate(UserDTO user);

        void CreateRole(Role role);
    }
}