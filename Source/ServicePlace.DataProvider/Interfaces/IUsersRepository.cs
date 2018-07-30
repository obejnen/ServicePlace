using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ServicePlace.Model;
using Microsoft.AspNetCore.Identity;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IUsersRepository
    {
        Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken);

        Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken);

        Task<User> FindByIdAsync(string userId);

        Task<User> FindByUserNameAsync(string userName);

        Task<User> FindByEmailAsync(string email);

        Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken);

        Task<IEnumerable<User>> GetAllUsers();

        void Dispose();
    }
}