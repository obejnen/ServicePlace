using System.Linq;
using ServicePlace.DataProvider.Mappers;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Interfaces;
using CommonModels = ServicePlace.Model;
using DataModels = ServicePlace.DataProvider.Entities;

namespace ServicePlace.DataProvider.Managers
{
    public class ProfileManager : IProfileManager
    {
        private readonly ApplicationContext _context;

        public ProfileManager()
        {
            _context = new ApplicationContext();
        }

        public async void CreateAsync(CommonModels.User user, string userId)
        {
            var profile = new ProfileMapper().MapToDataModel(user, userId);
            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();
        }

        public async void UpdateAsync(CommonModels.User user)
        {
            var modifiedProfile = new ProfileMapper().MapToDataModel(user);
            var profile = await _context.Profiles.FindAsync(user.Id);
            profile = modifiedProfile;
            await _context.SaveChangesAsync();
        }

        public async void DeleteAsync(CommonModels.User user)
        {
            var profile = new ProfileMapper().MapToDataModel(user);
            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}