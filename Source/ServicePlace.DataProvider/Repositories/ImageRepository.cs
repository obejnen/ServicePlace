using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Repositories
{
    public class ImageRepository : BaseRepository<Image>, IImageRepository
    {
        protected override IEnumerable<Expression<Func<Image, object>>> Includes =>
            new Expression<Func<Image, object>>[]
            {
            };

        public ImageRepository(ApplicationContext context) : base(context)
        {
        }
    }
}