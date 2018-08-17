using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Model.DataModels;


namespace ServicePlace.DataProvider.Repositories
{
    public class OrderResponseRepository : BaseRepository<OrderResponse>, IOrderResponseRepository
    {
        protected override IEnumerable<Expression<Func<OrderResponse, object>>> Includes =>
            new Expression<Func<OrderResponse, object>>[]
            {
                x => x.Creator.Profile,
                x => x.Order.Creator.Profile,
                x => x.Provider.Creator.Profile
            };

        public OrderResponseRepository(DbContext context) : base(context)
        {
        }
    }
}
