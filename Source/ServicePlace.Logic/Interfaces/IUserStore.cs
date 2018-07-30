using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ServicePlace.Model;

namespace ServicePlace.Logic.Interfaces
{
    public interface IUserStore
    {
        IQueryable<User> Users();

        Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken);

        Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken);

        Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken);

        Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken);

        Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken);

        Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken);

        Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken);

        Task SetNormalizedUserNameAsync(User user, string userName,
           CancellationToken cancellationToken);

        Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken);

        Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken);

        void Dispose();
    }
}