using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ServicePlace.DataProvider.Entities;

namespace ServicePlace.DataProvider.Managers
{
    public class RoleManager : RoleManager<Role>
    {
        public RoleManager(RoleStore<Role> store) : base(store)
        {
        }
    }
}