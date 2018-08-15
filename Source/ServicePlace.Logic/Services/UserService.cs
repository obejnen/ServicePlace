using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using ServicePlace.Model.LogicModels;
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

        public void Create(User user)
        {
            if (_repository.FindByEmail(user.Email) == null)
            {
                _repository.Create(user);
            }

        }

        public void Update(User user)
        {
            if (_repository.FindById(user.Id) != null)
            {
                _repository.Update(user);
            }
        }

        public void Delete(User user)
        {
            if (_repository.FindById(user.Id) != null)
            {
                _repository.Delete(user);
            }
        }

        public User FindById(object id) => _repository.FindById(id);

        public User FindByEmail(string email) =>
            _repository.FindByEmail(email);

        public User FindByUserName(string username) => _repository.FindByUserName(username);

        public ClaimsIdentity Authenticate(User user) => _repository.Authenticate(user);

        public IdentityResult CreateRole(Role role)
        {
            return _repository.CreateRole(role);
        }

        public void SetInitialData(User adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = new Role { Name = roleName };
                _repository.CreateRole(role);
            }
            Create(adminDto);
        }


        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}