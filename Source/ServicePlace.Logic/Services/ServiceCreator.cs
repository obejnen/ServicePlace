using Microsoft.AspNet.Identity.EntityFramework;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Managers;
using ServicePlace.DataProvider.Repositories;
using ServicePlace.Logic.Interfaces;
using DataModels = ServicePlace.DataProvider.Entities;

namespace ServicePlace.Logic.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService()
        {
            return new UserService(new IdentityRepository(
                new UserManager(new UserStore<DataModels.User>(new ApplicationContext()))
                , new RoleManager(new RoleStore<DataModels.Role>(new ApplicationContext()))
                , new ProfileManager()));
        }
    }
}