using Microsoft.AspNet.Identity;
using ServicePlace.DataProvider.Stores;
using ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Managers
{
    public class RoleManager : RoleManager<Role>
    {
        public RoleManager(RoleStore store) : base(store)
        {
        }
    }
}