using System;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Model;
using ServicePlace.Logic.Interfaces;

namespace ServicePlace.Logic.Stores
{
    public class UserStore : IUserStore<User>, IUserPasswordStore<User>, IUserStore
    {
        private readonly IUsersRepository _usersRepository;

        public UserStore(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
        }

        public IQueryable<User> Users => _usersRepository.GetAllUsers().Result.AsQueryable();

        IQueryable<User> IUserStore.Users() => Users;

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            return _usersRepository.CreateAsync(user, cancellationToken);
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            return _usersRepository.DeleteAsync(user, cancellationToken);
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _usersRepository.FindByIdAsync(userId);
        }

        public Task<User> FindByNameAsync(string userName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(userName), "Parameter normalizedUserName cannot be null or empty.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return _usersRepository.FindByUserNameAsync(userName);
        }

        public Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email), "Parameter normalizedUserName cannot be null or empty.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return _usersRepository.FindByEmailAsync(email);
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            return _usersRepository.UpdateAsync(user, cancellationToken);
        }

        public void Dispose()
        {
            _usersRepository.Dispose();
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.UserName);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(userName), "Parameter userName cannot be null or empty.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            user.UserName = userName;
            return Task.FromResult<object>(null);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(userName), "Parameter userName cannot be null or empty.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            user.UserName = userName;
            return Task.FromResult<object>(null);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            if (string.IsNullOrEmpty(passwordHash))
            {
                throw new ArgumentNullException(nameof(passwordHash), "Parameter passwordHash cannot be null or empty.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            user.PasswordHash = passwordHash;
            return Task.FromResult<object>(null);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Parameter user is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _context?.Dispose();
        //    }
        //}

        //public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        //{
        //    return Task.FromResult(user.Id.ToString());
        //}

        //public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        //{
        //    return Task.FromResult(user.UserName);
        //}

        //public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException(nameof(SetUserNameAsync));
        //}

        //public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException(nameof(GetNormalizedUserNameAsync));
        //}

        //public Task SetNormalizedUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        //{
        //    return Task.FromResult((object)null);
        //}

        //public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        //{
        //    _context.Add(user);

        //    await _context.SaveChangesAsync(cancellationToken);

        //    return await Task.FromResult(IdentityResult.Success);
        //}

        //public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException(nameof(UpdateAsync));
        //}

        //public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        //{
        //    _context.Remove(user);

        //    int i = await _context.SaveChangesAsync(cancellationToken);

        //    return await Task.FromResult(i == 1 ? IdentityResult.Success : IdentityResult.Failed());
        //}

        //public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        //{
        //    if (int.TryParse(userId, out int id))
        //    {
        //        return await _context.Users.FindAsync(id);
        //    }
        //    else
        //    {
        //        return await Task.FromResult((User)null);
        //    }
        //}

        //public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        //{
        //    return await _context.Users
        //                   .AsAsyncEnumerable()
        //                   .SingleOrDefault(p => p.UserName.Equals(normalizedUserName, StringComparison.OrdinalIgnoreCase), cancellationToken);
        //}

        //public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        //{
        //    user.PasswordHash = passwordHash;

        //    return Task.FromResult((object)null);
        //}

        //public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        //{
        //    return Task.FromResult(user.PasswordHash);
        //}

        //public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        //{
        //    return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
        //}
    }
}