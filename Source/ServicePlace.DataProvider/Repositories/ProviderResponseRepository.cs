using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Repositories
{
    public class ProviderResponseRepository : BaseRepository<ProviderResponse>, IProviderResponseRepository
    {
        protected override IEnumerable<Expression<Func<ProviderResponse, object>>> Includes =>
            new Expression<Func<ProviderResponse, object>>[]
            {
                x => x.Creator.Profile,
                x => x.Provider.Creator.Profile,
                x => x.Order.Creator.Profile
            };

        public ProviderResponseRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
