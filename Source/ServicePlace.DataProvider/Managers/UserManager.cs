using System.Data.Entity;
using ServicePlace.DataProvider.Entities;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace ServicePlace.DataProvider.Managers
{
    public class UserManager : UserManager<User>
    {
        public UserManager(IUserStore<User> store) : base(store)
        {
        }

        public override Task<User> FindByEmailAsync(string email)
        {
            return Users.Include(x => x.Profile).FirstOrDefaultAsync(y => y.Email == email);
        }

        public override Task<User> FindByNameAsync(string userName) =>
            Users.Include(x => x.Profile).FirstOrDefaultAsync(x => x.UserName == userName);

        public override Task<User> FindByIdAsync(string id) =>
            Users.Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
    }
}