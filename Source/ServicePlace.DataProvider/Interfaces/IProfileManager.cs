using System;
using ServicePlace.Model;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IProfileManager : IDisposable
    {
        void CreateAsync(User item, string id);

        void UpdateAsync(User item);

        void DeleteAsync(User item);
    }
}