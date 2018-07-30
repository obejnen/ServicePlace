using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ServicePlace.Model;
using Microsoft.AspNetCore.Identity;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IRolesRepository
    {
        Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken);

        Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken);

        Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken);

        Task<Role> FindByIdAsync(string roleId);

        Task<Role> FindByNameAsync(string roleName);

        Task<IEnumerable<Role>> GetAllRoles();

        void Dispose();
    }
}