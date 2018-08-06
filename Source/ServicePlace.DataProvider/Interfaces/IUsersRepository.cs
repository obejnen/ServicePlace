using System.Threading.Tasks;
using ServicePlace.Model;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IUsersRepository<T, TId, TResult> : IRepository<T, TId, TResult> where T : class
    {
        Task<User> FindByUserNameAsync(string userName);

        Task<User> FindByEmailAsync(string email);
    }
}