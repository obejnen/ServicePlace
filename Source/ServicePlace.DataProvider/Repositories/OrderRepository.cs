//using AutoMapper;
//using System.Linq;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;
//using CommonModels = ServicePlace.Model;
//using ServicePlace.DataProvider.DbContexts;
//using ServicePlace.DataProvider.Interfaces;
//using ServicePlace.DataProvider.Mappers;
//using ServicePlace.Common.Enums;
//using System.Data.Entity;

//namespace ServicePlace.DataProvider.Repositories
//{
//    public class OrderRepository : IOrdersRepository<CommonModels.Order, int, ResponseType>
//    {
//        private readonly ApplicationContext _context;
//        private readonly OrderMapper _mapper;
//        private readonly IMapper _autoMapper;

//        public OrderRepository(ApplicationContext context, IMapper autoMapper)
//        {
//            _context = context;
//            _mapper = new OrderMapper();
//            _autoMapper = autoMapper;
//        }

//        public Task<IEnumerable<CommonModels.Order>> GetAll()
//        {
//            //Mapper.Reset();
//            //Mapper.Initialize(cfg => cfg.CreateMap<List<DataModels.Order>, List<CommonModels.Order>>());
//            //var collection =
//            //    Mapper.Map<List<DataModels.Order>, List<CommonModels.Order>>(_context.Orders.ToList());
//            var list = _context.Orders.Include(x => x.Creator).Select(x => _mapper.MapToCommonModel(x)).ToList();
//            return Task.FromResult(_autoMapper.Map<IEnumerable<CommonModels.Order>>(list));
//        }

//        public Task<ResponseType> CreateUserAsync(CommonModels.Order model, CancellationToken cancellationToken)
//        {
//            var order = _mapper.MapToDataModel(model);
//            _context.Orders.Add(order);
//            return Task.FromResult(_context.SaveChangesAsync(cancellationToken).Result > 0
//                ? ResponseType.Success
//                : ResponseType.Failded);
//        }

//        public Task<ResponseType> DeleteUserAsync(CommonModels.Order model, CancellationToken cancellationToken)
//        {
//            var order = _mapper.MapToDataModel(model);
//            _context.Orders.Remove(order);
//            return Task.FromResult(_context.SaveChangesAsync(cancellationToken).Result > 0
//                ? ResponseType.Success
//                : ResponseType.Failded);
//        }

//        public Task<ResponseType> UpdateUserAsync(CommonModels.Order model, CancellationToken cancellationToken)
//        {
//            var newOrder = _mapper.MapToDataModel(model);
//            var order = _context.Orders.FirstOrDefault(x => x.Id == newOrder.Id);
//            order = newOrder;
//            return Task.FromResult(_context.SaveChangesAsync(cancellationToken).Result > 0
//                ? ResponseType.Success
//                : ResponseType.Failded);
//        }

//        public Task<CommonModels.Order> FindByIdAsync(int id)
//        {
//            var order = _context.Orders.Find(id);
//            return Task.FromResult(_mapper.MapToCommonModel(order));
//        }

//        public Task<IEnumerable<CommonModels.Order>> SearchAsync(string search)
//        {
//            var orders = _context.Orders.Where(x => x.Title.Contains(search) || x.Body.Contains(search));
//            return Task.FromResult(
//                _autoMapper.Map<IEnumerable<CommonModels.Order>>(orders.Select(x => _mapper.MapToCommonModel(x))));
//        }

//        public Task<IEnumerable<CommonModels.Order>> Take(int skip, int count)
//        {
//            var orders = _context.Orders.Skip(skip).Take(count);
//            return Task.FromResult(
//                _autoMapper.Map<IEnumerable<CommonModels.Order>>(orders.Select(x => _mapper.MapToCommonModel(x))));
//        }

//        public void Dispose()
//        {
//            _context.Dispose();
//        }

//    //public void AddOrder(CommonModels.Order order)
//    //    {
//    //        //Mapper.Reset();
//    //        //Mapper.Initialize(cfg => cfg.CreateMap<CommonModels.Order, DataModels.Order>());
//    //        //var model = Mapper.Map<CommonModels.Order, DataModels.Order>(order);
//    //        var model = _mapper.MapToDataModel(order);
//    //        _context.Orders.Add(model);
//    //        _context.SaveChanges();
//    //    }

//    //    public void RemoveOrder(int id)
//    //    {
//    //        _context.Orders.Remove(_context.Orders.FirstOrDefault(order => order.Id == id));
//    //        _context.SaveChanges();
//    //    }

//    //    public void UpdateOrder(CommonModels.Order newOrder)
//    //    {
//    //        var order = _context.Orders.FirstOrDefault(ord => ord.Id == newOrder.Id);
//    //        Mapper.Reset();
//    //        Mapper.Initialize(cfg => cfg.CreateMap<CommonModels.Order, DataModels.Order>());
//    //        var updatedOrder = Mapper.Map<CommonModels.Order, DataModels.Order>(newOrder);
//    //        order = updatedOrder;
//    //        _context.SaveChanges();
//    //    }

//    //    public CommonModels.Order GetOrder(int id)
//    //    {
//    //        var model = _context.Orders.FirstOrDefault(ord => ord.Id == id);
//    //        CommonModels.Order order = null;
//    //        if(model != null)
//    //        {
//    //            Mapper.Initialize(cfg => cfg.CreateMap<DataModels.Order, CommonModels.Order>());
//    //            order = Mapper.Map<DataModels.Order, CommonModels.Order>(model);
//    //            Mapper.Reset();
//    //        }

//    //        return order;
//    //    }
//    }
//}
