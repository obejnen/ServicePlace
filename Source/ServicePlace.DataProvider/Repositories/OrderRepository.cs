using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using DataModels = ServicePlace.DataProvider.Models;
using CommonModels = ServicePlace.Model;
using ServicePlace.DataProvider.DbContexts;

namespace ServicePlace.DataProvider.Repositories
{
    public class OrderRepository
    {
        private readonly ApplicationContext _context;

        public OrderRepository()
        {
            _context = new ApplicationContext();
        }

        public IEnumerable<CommonModels.Order> GetAllOrders()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<List<DataModels.Order>, List<CommonModels.Order>>());
            var collection =
                Mapper.Map<List<DataModels.Order>, List<CommonModels.Order>>(_context.Orders.ToList());
            return collection;
        }

    public void AddOrder(CommonModels.Order order)
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<CommonModels.Order, DataModels.Order>());
            var model = Mapper.Map<CommonModels.Order, DataModels.Order>(order);
            _context.Orders.Add(model);
            _context.SaveChanges();
        }

        public void RemoveOrder(int id)
        {
            _context.Orders.Remove(_context.Orders.FirstOrDefault(order => order.Id == id));
            _context.SaveChanges();
        }

        public void UpdateOrder(CommonModels.Order newOrder)
        {
            var order = _context.Orders.FirstOrDefault(ord => ord.Id == newOrder.Id);
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<CommonModels.Order, DataModels.Order>());
            var updatedOrder = Mapper.Map<CommonModels.Order, DataModels.Order>(newOrder);
            order = updatedOrder;
            _context.SaveChanges();
        }

        public CommonModels.Order GetOrder(int id)
        {
            var model = _context.Orders.FirstOrDefault(ord => ord.Id == id);
            CommonModels.Order order = null;
            if(model != null)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DataModels.Order, CommonModels.Order>());
                order = Mapper.Map<DataModels.Order, CommonModels.Order>(model);
                Mapper.Reset();
            }

            return order;
        }
    }
}
