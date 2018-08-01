using System.Threading.Tasks;
using ServicePlace.Model;
using Microsoft.AspNetCore.Identity;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IRolesRepository : IRepository<Role, string, IdentityResult>
    {
        Task<Role> FindByNameAsync(string name);
    }
}