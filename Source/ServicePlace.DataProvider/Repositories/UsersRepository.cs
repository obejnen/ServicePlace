using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CommonModels = ServicePlace.Model;
using DataModels = ServicePlace.DataProvider.Models;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Interfaces;

namespace ServicePlace.DataProvider.Repositories
{
    public class UsersRepository : IUsersRepository<CommonModels.User, string, IdentityResult>
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public UsersRepository(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<IdentityResult> CreateAsync(CommonModels.User user, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<DataModels.User>(user);
            _context.Add(model);
            return Task.FromResult(_context.SaveChangesAsync(cancellationToken).Result > 0
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError
                {
                    Code = string.Empty,
                    Description = $"The user with email {user.Email} could not be inserted in the dbo.Users table."
                }));
        }

        public Task<IdentityResult> DeleteAsync(CommonModels.User user, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<DataModels.User>(user);
            _context.Remove(model);
            return Task.FromResult(_context.SaveChangesAsync(cancellationToken).Result > 0
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError
                {
                    Code = string.Empty,
                    Description = $"The user with email {user.Email} could not be deleted from the dbo.Users table."
                }));
        }

        public Task<CommonModels.User> FindByIdAsync(string userId)
        {
            var model = _context.Users.FirstOrDefault(x => x.Id == userId);
            return Task.FromResult(_mapper.Map<CommonModels.User>(model));
        }

        public Task<CommonModels.User> FindByUserNameAsync(string userName)
        {
            var model = _context.Users.FirstOrDefault(x => x.UserName == userName);
            return Task.FromResult(_mapper.Map<CommonModels.User>(model));
        }

        public Task<CommonModels.User> FindByNameAsync(string name)
        {
            var model = _context.Users.FirstOrDefault(x => x.UserName == name);
            return Task.FromResult(_mapper.Map<CommonModels.User>(model));
        }

        public Task<CommonModels.User> FindByEmailAsync(string email)
        {
            var model = _context.Users.FirstOrDefault(x => x.Email == email);
            return Task.FromResult(_mapper.Map<CommonModels.User>(model));
        }

        public Task<IdentityResult> UpdateAsync(CommonModels.User user, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<DataModels.User>(user);
            DataModels.User userToEdit = _context.Users.SingleOrDefault(x => x.Id == user.Id);
            userToEdit = model;
            return Task.FromResult(_context.SaveChangesAsync(cancellationToken).Result > 0
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError
                {
                    Code = string.Empty,
                    Description = $"The user with email {user.Email} could not be updated in the dbo.Users table."
                }));
        }

        public Task<IEnumerable<CommonModels.User>> GetAll()
        {
            return Task.FromResult(_mapper.Map<IEnumerable<CommonModels.User>>(_context.Users));
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
