﻿using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq.Expressions;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        protected override IEnumerable<Expression<Func<Order, object>>> Includes =>
            new Expression<Func<Order, object>>[]
                {
                    x => x.Creator.Profile,
                    x => x.Images
                };

        public OrderRepository(DbContext context) : base(context)
        {
        }

        public void CloseOrder(int id)
        {
            var order = Query.SingleOrDefault(x => x.Id == id);
            if(order != null)
            {
                order.Closed = true;
                Update(order);
            }
        }

        public IEnumerable<Order> Take(int skip, int count)
        {
            return Query
                .OrderBy(x => x.UpdatedAt)
                .Skip(skip)
                .Take(count)
                .ToList();
        }
    }
}
