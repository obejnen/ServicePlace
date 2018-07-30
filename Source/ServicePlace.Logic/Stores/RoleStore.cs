using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ServicePlace.Logic.Interfaces;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Model;

namespace ServicePlace.Logic.Stores
{
    public class RoleStore : IQueryableRoleStore<Role>, IRoleStore
    {
        private readonly IRolesRepository _rolesRepository;

        public RoleStore(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public IQueryable<Role> Roles => _rolesRepository.GetAllRoles().Result.AsQueryable();

        IQueryable<Role> IRoleStore.Roles() => Roles;

        public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role), "Parameter role is not set to an instance of an object.");
            }

            return _rolesRepository.CreateAsync(role, cancellationToken);
        }

        public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role), "Parameter role is not set to an instance of an object.");
            }

            return _rolesRepository.UpdateAsync(role, cancellationToken);
        }

        public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role), "Parameter role is not set to an instance of an object.");
            }

            return _rolesRepository.DeleteAsync(role, cancellationToken);
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role), "Parameter role is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(role.Id);
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role), "Parameter role is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role), "Parameter role is not set to an instance of an object.");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException(nameof(roleName), "Parameter roleName cannot be null or empty.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            role.Name = roleName;
            return Task.FromResult<object>(null);
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role), "Parameter role is not set to an instance of an object.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(Role role, string name, CancellationToken cancellationToken)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role), "Parameter role is not set to an instance of an object.");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Parameter name cannot be null or empty.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            role.Name = name;
            return Task.FromResult<object>(null);
        }

        public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                throw new ArgumentNullException(nameof(roleId), "Parameter roleId cannot be null or empty.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return _rolesRepository.FindByIdAsync(roleId);
        }

        public Task<Role> FindByNameAsync(string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Parameter name cannot be null or empty.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return _rolesRepository.FindByNameAsync(name);
        }

        public void Dispose()
        {
            _rolesRepository.Dispose();
        }
    }
}


//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;
//using ServicePlace.Model;

//namespace ServicePlace.Logic.Stores
//{
//    public class RoleStore : IRoleStore<UserRole>
//    {
//        public void Dispose()
//        {
//        }

//        public Task<IdentityResult> CreateAsync(UserRole role, CancellationToken cancellationToken)
//        {
//            throw new System.NotImplementedException();
//        }

//        public Task<IdentityResult> UpdateAsync(UserRole role, CancellationToken cancellationToken)
//        {
//            throw new System.NotImplementedException();
//        }

//        public Task<IdentityResult> DeleteAsync(UserRole role, CancellationToken cancellationToken)
//        {
//            throw new System.NotImplementedException();
//        }

//        public Task<string> GetRoleIdAsync(UserRole role, CancellationToken cancellationToken)
//        {
//            throw new System.NotImplementedException();
//        }

//        public Task<string> GetRoleNameAsync(UserRole role, CancellationToken cancellationToken)
//        {
//            throw new System.NotImplementedException();
//        }

//        public Task SetRoleNameAsync(UserRole role, string roleName, CancellationToken cancellationToken)
//        {
//            throw new System.NotImplementedException();
//        }

//        public Task<string> GetNormalizedRoleNameAsync(UserRole role, CancellationToken cancellationToken)
//        {
//            throw new System.NotImplementedException();
//        }

//        public Task SetNormalizedRoleNameAsync(UserRole role, string name, CancellationToken cancellationToken)
//        {
//            throw new System.NotImplementedException();
//        }

//        public Task<UserRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
//        {
//            throw new System.NotImplementedException();
//        }

//        public Task<UserRole> FindByNameAsync(string name, CancellationToken cancellationToken)
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}
