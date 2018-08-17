using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Repositories
{
    public class OrderCategoryRepository : BaseRepository<OrderCategory>, IOrderCategoryRepository
    {
        protected override IEnumerable<Expression<Func<OrderCategory, object>>> Includes =>
            new Expression<Func<OrderCategory, object>>[]
            {
                x => x.Orders
            };

        public OrderCategoryRepository(DbContext context) : base(context)
        {
        }
    }
}