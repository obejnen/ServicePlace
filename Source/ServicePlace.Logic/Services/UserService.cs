using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using CommonModels = ServicePlace.Model;
using ServicePlace.Logic.Interfaces;
using ServicePlace.DataProvider.Interfaces;

namespace ServicePlace.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IIdentityRepository _repository;

        public UserService(IIdentityRepository repository)
        {
            _repository = repository;
        }

        public IdentityResult CreateUser(CommonModels.User user) =>
            _repository.FindByEmail(user.Email) == null
                ? _repository.CreateUser(user)
                : IdentityResult.Failed($"User with email {user.Email} already exists");

        public IdentityResult UpdateUser(CommonModels.User user) =>
            _repository.FindById(user.Id) == null
                ? IdentityResult.Failed("Cannot find user")
                : _repository.UpdateUser(user);

        public IdentityResult DeleteUser(CommonModels.User user) =>
            _repository.FindById(user.Id) == null
                ? IdentityResult.Failed("Cannot find user")
                : _repository.DeleteUser(user);

        public CommonModels.User FindById(string id) => _repository.FindById(id);

        public CommonModels.User FindByEmail(string email) =>
            _repository.FindByEmail(email);

        public CommonModels.User FindByUserName(string username) => _repository.FindByUserName(username);

        public ClaimsIdentity Authenticate(CommonModels.User user) => _repository.Authenticate(user);

        public IdentityResult CreateRole(CommonModels.Role role)
        {
            return _repository.CreateRole(role);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}