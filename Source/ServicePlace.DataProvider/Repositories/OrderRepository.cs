using AutoMapper;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using ServicePlace.DataProvider.Mappers;
using CommonModels = ServicePlace.Model;
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

        public IEnumerable<CommonModels.Order> GetAll()
        {
            var result = _context.Orders.Include(x => x.Creator.Profile).ToList()
                .Select(x => _mapper.MapToCommonModel(x));
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<List<CommonModels.Order>, IEnumerable<CommonModels.Order>>());
            return Mapper.Map<IEnumerable<CommonModels.Order>>(result);
        }

        public void Create(CommonModels.Order model)
        {
            var creator = _context.Users.FirstOrDefault(x => x.Id == model.Creator.Id);
            var order = _mapper.MapToDataModel(model, creator);
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void Delete(CommonModels.Order model)
        {
            var creator = _context.Users.FirstOrDefault(x => x.Id == model.Creator.Id);
            var order = _mapper.MapToDataModel(model, creator);
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public void Update(CommonModels.Order model)
        {
            var creator = _context.Users.FirstOrDefault(x => x.Id == model.Creator.Id);
            var newOrder = _mapper.MapToDataModel(model, creator);
            _context.Orders.AddOrUpdate(newOrder);
            _context.SaveChanges();
        }

        public CommonModels.Order FindById(object id)
        {
            var order = _context.Orders.Find(id);
            return _mapper.MapToCommonModel(order);
        }

        public IEnumerable<CommonModels.Order> Search(string search)
        {
            var orders = _context.Orders.Where(x => x.Title.Contains(search) || x.Body.Contains(search));
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<List<CommonModels.Order>, IEnumerable<CommonModels.Order>>());
            return Mapper.Map<IEnumerable<CommonModels.Order>>(orders.Select(x => _mapper.MapToCommonModel(x)));
        }

        public IEnumerable<CommonModels.Order> Take(int skip, int count)
        {
            var orders = _context.Orders.Skip(skip).Take(count);
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<List<CommonModels.Order>, IEnumerable<CommonModels.Order>>());
            return Mapper.Map<IEnumerable<CommonModels.Order>>(orders.Select(x => _mapper.MapToCommonModel(x)));
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
