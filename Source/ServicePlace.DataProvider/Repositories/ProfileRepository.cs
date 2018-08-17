using System.Data.Entity;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Repositories
{
    public class ProfileRepository : BaseRepository<Profile> : IProfileRepository
    {
        public ProfileManager(DbContext context) : base(context)
        {
        }
    }
}