using System;
using ServicePlace.Model.LogicModels;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IProfileManager : IDisposable
    {
        void Create(User item, string id);

        void Update(User item);

        void Delete(User item);
    }
}