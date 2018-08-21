using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Repositories
{
    class ProviderCategoryRepository : BaseRepository<ProviderCategory>, IProviderCategoryRepository
    {
        protected override IEnumerable<Expression<Func<ProviderCategory, object>>> Includes =>
            new Expression<Func<ProviderCategory, object>>[]
            {
                x => x.Providers
            };

        public ProviderCategoryRepository(ApplicationContext context) : base(context)
        {

        }
    }
}
