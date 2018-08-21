using Microsoft.AspNet.Identity.EntityFramework;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Stores
{
    public class UserStore : UserStore<User>
    {
        public UserStore(ApplicationContext context) : base(context)
        {

        }
    }
}