using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ServicePlace.Common;
using ServicePlace.Logic.Interfaces.Services;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Model.DataModels;
using ServicePlace.Model.DTOModels;

namespace ServicePlace.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IIdentityRepository _repository;
        private readonly IProfileRepository _profileRepository;
        private readonly IContextProvider _contextProvider;

        public UserService(IIdentityRepository repository,
            IProfileRepository profileRepository,
            IContextProvider contextProvider)
        {
            _repository = repository;
            _profileRepository = profileRepository;
            _contextProvider = contextProvider;
        }

        public void Create(UserDTO userDto)
        {
            if (_repository.GetBy(x => x.Email == userDto.Email).Any()) return;
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = userDto.UserName,
                Avatar = new Image { Url = userDto.Avatar ?? Constants.DefaultAvatar },
                Email = userDto.Email,
                PasswordHash = userDto.Password,
                Profile = new Profile
                {
                    Name = userDto.Name
                }
            };
            _repository.Create(user);
            _repository.AddToRole(user.Id, Constants.UserRoleName);
            _contextProvider.CommitChanges();
        }

        public void Update(UserDTO userDto)
        {
            var user = _repository.GetBy(x => x.Id == userDto.Id).SingleOrDefault();
            if (user == null) return;
            user.Profile.Id = userDto.Id;
            user.Avatar = new Image {Url = userDto.Avatar};
            user.Profile.Name = userDto.Name;
            user.Id = userDto.Id;
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            _profileRepository.Update(user.Profile);
            _repository.Update(user);
            _contextProvider.CommitChanges();
        }

        public void Delete(UserDTO userDto)
        {
            var user = _repository.GetBy(x => x.Id == userDto.Id).SingleOrDefault();
            if (user == null) return;
            _profileRepository.Delete(user.Profile);
            _repository.Delete(user);
            _contextProvider.CommitChanges();
        }

        public UserDTO Get(object id)
        {
            var user = _repository.GetBy(x => x.Id == (string)id).SingleOrDefault();
            return user == null
                ? null 
                : new UserDTO
                    {
                        Id = user.Id,
                        Avatar = user.Avatar.Url,
                        UserName = user.UserName,
                        Email = user.Email,
                        Name = user.Profile.Name,
                    };
        }

        public IEnumerable<User> GetAll() => _repository.GetAll();

        public User FindByEmail(string email)
        {
            return _repository.GetBy(x => x.Email == email).SingleOrDefault();
        }

        public User FindByUserName(string username)
        {
            return _repository.GetBy(x => x.UserName == username).SingleOrDefault();
        }

        public ClaimsIdentity Authenticate(UserDTO user) => _repository.Authenticate(user.UserName, user.Password);

        public void CreateRole(string roleName)
        {
            var role = new Role()
            {
                Name = roleName
            };
            _repository.CreateRole(role);
            _contextProvider.CommitChanges();
        }

        public bool IsInRole(string userId, string roleName) => _repository.IsInRole(userId, roleName);

        public void AddToRole(string userId, string roleName)
        {
            _repository.AddToRole(userId, roleName);
        }

        public void RemoveFromRole(string userId, string roleName)
        {
            _repository.RemoveFromRole(userId, roleName);
        }
    }
}