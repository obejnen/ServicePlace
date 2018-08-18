using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Interfaces;

namespace ServicePlace.DataProvider.ContextProviders
{
    public class ContextProvider : IContextProvider
    {
        private readonly ApplicationContext _context;

        public ContextProvider(ApplicationContext context)
        {
            _context = context;
        }
        public void CommitChanges() => _context.SaveChanges();
    }
}