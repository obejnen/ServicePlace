using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Repositories
{
    public class ProfileRepository : BaseRepository<Profile>, IProfileRepository
    {
        protected override IEnumerable<Expression<Func<Profile, object>>> Includes =>
            new Expression<Func<Profile, object>>[]
            {

            };
        public ProfileRepository(ApplicationContext context) : base(context)
        {
        }
    }
}