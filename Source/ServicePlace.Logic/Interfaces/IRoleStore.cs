using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ServicePlace.Model;

namespace ServicePlace.Logic.Interfaces
{
    public interface IRoleStore
    {
        IQueryable<Role> Roles();

        Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken);

        Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken);

        Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken);

        Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken);

        Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken);

        Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken);

        Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken);

        Task SetNormalizedRoleNameAsync(Role role, string name,
            CancellationToken cancellationToken);

        Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken);

        Task<Role> FindByNameAsync(string name, CancellationToken cancellationToken);

        void Dispose();
    }
}