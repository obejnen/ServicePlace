using AutoMapper;
using System.Linq;
using System.Threading;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonModels = ServicePlace.Model;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.DataProvider.Mappers;
using ServicePlace.Common.Enums;

namespace ServicePlace.DataProvider.Repositories
{
    public class OrderRepository : IOrderRepository<CommonModels.Order, int, ResponseType>
    {
        private readonly ApplicationContext _context;
        private readonly OrderMapper _mapper;

        public OrderRepository(ApplicationContext context)
        {
            _context = context;
            _mapper = new OrderMapper();
        }

        public Task<IEnumerable<CommonModels.Order>> GetAll()
        {
            var list = _context.Orders.Include(x => x.Creator).Select(x => _mapper.MapToCommonModel(x)).ToList();
            return Task.FromResult(Mapper.Map<IEnumerable<CommonModels.Order>>(list));
        }

        public Task<ResponseType> CreateAsync(CommonModels.Order model, CancellationToken cancellationToken)
        {
            var order = _mapper.MapToDataModel(model);
            _context.Orders.Add(order);
            return Task.FromResult(_context.SaveChangesAsync(cancellationToken).Result > 0
                ? ResponseType.Success
                : ResponseType.Failed);
        }

        public Task<ResponseType> DeleteAsync(CommonModels.Order model, CancellationToken cancellationToken)
        {
            var order = _mapper.MapToDataModel(model);
            _context.Orders.Remove(order);
            return Task.FromResult(_context.SaveChangesAsync(cancellationToken).Result > 0
                ? ResponseType.Success
                : ResponseType.Failed);
        }

        public Task<ResponseType> UpdateAsync(CommonModels.Order model, CancellationToken cancellationToken)
        {
            var newOrder = _mapper.MapToDataModel(model);
            var order = _context.Orders.FirstOrDefault(x => x.Id == newOrder.Id);
            order = newOrder;
            return Task.FromResult(_context.SaveChangesAsync(cancellationToken).Result > 0
                ? ResponseType.Success
                : ResponseType.Failed);
        }

        public Task<CommonModels.Order> FindByIdAsync(int id)
        {
            var order = _context.Orders.Find(id);
            return Task.FromResult(_mapper.MapToCommonModel(order));
        }

        public Task<IEnumerable<CommonModels.Order>> SearchAsync(string search)
        {
            var orders = _context.Orders.Where(x => x.Title.Contains(search) || x.Body.Contains(search));
            return Task.FromResult(
                Mapper.Map<IEnumerable<CommonModels.Order>>(orders.Select(x => _mapper.MapToCommonModel(x))));
        }

        public Task<IEnumerable<CommonModels.Order>> Take(int skip, int count)
        {
            var orders = _context.Orders.Skip(skip).Take(count);
            return Task.FromResult(
                Mapper.Map<IEnumerable<CommonModels.Order>>(orders.Select(x => _mapper.MapToCommonModel(x))));
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
