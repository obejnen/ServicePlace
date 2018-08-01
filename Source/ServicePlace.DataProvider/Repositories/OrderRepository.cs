using System;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataModels = ServicePlace.DataProvider.Models;
using CommonModels = ServicePlace.Model;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.DataProvider.Mappers;
using ServicePlace.Common.Enums;

namespace ServicePlace.DataProvider.Repositories
{
    public class OrderRepository : IOrdersRepository<CommonModels.Order, int, ResponseType>
    {
        private readonly ApplicationContext _context;
        private readonly OrderMapper _mapper;
        private readonly IMapper _autoMapper;

        public OrderRepository(ApplicationContext context, IMapper autoMapper)
        {
            _context = context;
            _mapper = new OrderMapper();
            _autoMapper = autoMapper;
        }

        public Task<IEnumerable<CommonModels.Order>> GetAll()
        {
            //Mapper.Reset();
            //Mapper.Initialize(cfg => cfg.CreateMap<List<DataModels.Order>, List<CommonModels.Order>>());
            //var collection =
            //    Mapper.Map<List<DataModels.Order>, List<CommonModels.Order>>(_context.Orders.ToList());
            var list = _context.Orders.Select(x => _mapper.MapToCommonModel(x)).ToList();
            return Task.FromResult(_autoMapper.Map<IEnumerable<CommonModels.Order>>(list));
        }

        public Task<ResponseType> CreateAsync(CommonModels.Order model, CancellationToken cancellationToken)
        {
            var order = _mapper.MapToDataModel(model);
            _context.Orders.Add(order);
            return Task.FromResult(_context.SaveChangesAsync(cancellationToken).Result > 0
                ? ResponseType.Success
                : ResponseType.Failded);
        }

    public void AddOrder(CommonModels.Order order)
        {
            //Mapper.Reset();
            //Mapper.Initialize(cfg => cfg.CreateMap<CommonModels.Order, DataModels.Order>());
            //var model = Mapper.Map<CommonModels.Order, DataModels.Order>(order);
            var model = _mapper.MapToDataModel(order);
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
