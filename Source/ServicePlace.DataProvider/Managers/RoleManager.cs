using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Managers
{
    public class RoleManager : RoleManager<Role>
    {
        public RoleManager(IRoleStore<Role, string> store) : base(store)
        {
        }
    }
}