
using ServicePlace.DataProvider.DbContexts;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IContextProvider
    {
        void CommitChanges();
    }
}
