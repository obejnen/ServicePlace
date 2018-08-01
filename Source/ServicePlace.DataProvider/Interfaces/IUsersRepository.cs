using System.Threading.Tasks;
using ServicePlace.Model;
using Microsoft.AspNetCore.Identity;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IUsersRepository : IRepository<User, string, IdentityResult>
    {
        Task<User> FindByUserNameAsync(string userName);

        Task<User> FindByEmailAsync(string email);
    }
}