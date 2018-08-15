using Microsoft.AspNet.Identity.EntityFramework;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Stores
{
    public class RoleStore : RoleStore<Role>
    {
        public RoleStore(ApplicationContext context) : base(context)
        {

        }
    }
}