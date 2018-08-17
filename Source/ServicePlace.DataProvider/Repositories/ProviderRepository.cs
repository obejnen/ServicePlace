using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.Model.DataModels;
using ServicePlace.DataProvider.Interfaces;

namespace ServicePlace.DataProvider.Repositories
{
    public class ProviderRepository : BaseRepository<Provider>, IProviderRepository
    {
        protected override IEnumerable<Expression<Func<Provider, object>>> Includes =>
            new Expression<Func<Provider, object>>[]
            {
                x => x.Creator.Profile,
                x => x.Category
            };

        public ProviderRepository(ApplicationContext context) : base(context)
        {
        }

        public IEnumerable<Provider> Take(int skip, int count)
        {
            return Query.OrderBy(x => x.CreatedAt).Skip(skip).Take(count).ToList();
        }
    }
}