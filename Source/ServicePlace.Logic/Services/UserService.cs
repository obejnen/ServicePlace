using System;
using System.Linq;
using System.Security.Claims;
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
                Email = userDto.Email,
                PasswordHash = userDto.Password,
                Profile = new Profile
                {
                    Name = userDto.Name
                }
            };
            _repository.Create(user);
            _repository.AddToRole(user.Id, userDto.Role);
            _profileRepository.Create(new Profile
            {
                Id = user.Id,
                Name = userDto.Name
            });
            _contextProvider.CommitChanges();
        }

        public void Update(UserDTO userDto)
        {
            var user = _repository.GetBy(x => x.Id == userDto.Id).SingleOrDefault();
            if (user == null) return;
            user.Profile.Id = userDto.Id;
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
                        UserName = user.UserName,
                        Email = user.Email,
                        Name = user.Profile.Name,
                    };
        }

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
    }
}