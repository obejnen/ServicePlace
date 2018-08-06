using System.Data.Entity;
using System.Linq;
using ServicePlace.Model.DataModels;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using ServicePlace.DataProvider.Stores;

namespace ServicePlace.DataProvider.Managers
{
    public class UserManager : UserManager<User>
    {
        public UserManager(UserStore store) : base(store)
        {
        }

        public override Task<User> FindByEmailAsync(string email)
        {
            return Task.FromResult(Users.Include(x => x.Profile).FirstOrDefault(y => y.Email == email));
        }

        public override Task<User> FindByNameAsync(string userName) =>
            Users.Include(x => x.Profile).FirstOrDefaultAsync(x => x.UserName == userName);

        public override Task<User> FindByIdAsync(string id) =>
            Users.Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
    }
}