using ServicePlace.DataProvider.Repositories;
using ServicePlace.Logic.Interfaces;

namespace ServicePlace.Logic.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService()
        {
            return new UserService(new IdentityRepository());
        }
    }
}