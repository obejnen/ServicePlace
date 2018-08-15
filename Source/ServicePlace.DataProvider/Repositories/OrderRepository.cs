using AutoMapper;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using ServicePlace.DataProvider.Mappers;
using ServicePlace.Model.LogicModels;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Interfaces;

namespace ServicePlace.DataProvider.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _context;
        private readonly OrderMapper _mapper;

        public OrderRepository(ApplicationContext context)
        {
            _context = context;
            _mapper = new OrderMapper();
        }

        public int GetOrdersCount() => _context.Orders.Count();

        public IEnumerable<Order> GetAll()
        {
            var result = _context.Orders.Include(x => x.Creator.Profile).ToList()
                .Select(x => _mapper.MapToCommonModel(x));
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<List<Order>, IEnumerable<Order>>());
            return Mapper.Map<IEnumerable<Order>>(result);
        }

        public void Create(Order model)
        {
            var creator = _context.Users.FirstOrDefault(x => x.Id == model.Creator.Id);
            var order = _mapper.MapToDataModel(model, creator);
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void Delete(Order model)
        {
            var creator = _context.Users.FirstOrDefault(x => x.Id == model.Creator.Id);
            var order = _mapper.MapToDataModel(model, creator);
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public void Update(Order model)
        {
            var creator = _context.Users.FirstOrDefault(x => x.Id == model.Creator.Id);
            var newOrder = _mapper.MapToDataModel(model, creator);
            _context.Orders.AddOrUpdate(newOrder);
            _context.SaveChanges();
        }

        public void Close(int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);
            if(order != null)
            {
                order.Closed = true;
                _context.Orders.AddOrUpdate(order);
                _context.SaveChanges();
            }
        }

        public Order FindById(object id)
        {
            var order = _context.Orders.Include(x => x.Creator.Profile).FirstOrDefault(x => x.Id == (int)id);
            return _mapper.MapToCommonModel(order);
        }

        public IEnumerable<Order> Search(string search)
        {
            var orders = _context.Orders.Include(x => x.Creator.Profile).Where(x => x.Title.Contains(search) || x.Body.Contains(search)).ToList();
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<List<Order>, IEnumerable<Order>>());
            return Mapper.Map<IEnumerable<Order>>(orders.Select(x => _mapper.MapToCommonModel(x)));
        }

        public IEnumerable<Order> Take(int skip, int count)
        {
            var orders = _context.Orders.Include(x => x.Creator.Profile).OrderBy(x => x.CreatedAt).Skip(skip).Take(count).ToList();
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<List<Order>, IEnumerable<Order>>());
            return Mapper.Map<IEnumerable<Order>>(orders.Select(x => _mapper.MapToCommonModel(x)));
        }

        public Order GetOrderProvider(int providerId, int orderId)
        {
            var list = _context.OrderResponses
                .Include(x => x.Order)
                .Include(x => x.Provider)
                .FirstOrDefault(x => x.Order.Id == orderId && x.Provider.Id == providerId);
            return list == null
                ? null
                : _mapper.MapToCommonModel(list.Order);
        }

        public IEnumerable<Order> GetUserOrders(string userId)
        {
            return _context
                .Orders
                .Include(x => x.Creator.Profile)
                .Where(x => x.Creator.Id == userId)
                .ToList()
                .Select(x => _mapper.MapToCommonModel(x));
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
