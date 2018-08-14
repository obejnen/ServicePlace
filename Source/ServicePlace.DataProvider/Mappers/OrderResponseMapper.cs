using System.Data.Entity;
using System.Linq;
using ServicePlace.DataProvider.DbContexts;
using DataModels = ServicePlace.Model.DataModels;
using LogicModels = ServicePlace.Model.LogicModels;

namespace ServicePlace.DataProvider.Mappers
{
    public class OrderResponseMapper
    {
        private readonly ApplicationContext _context;

        public OrderResponseMapper(ApplicationContext context)
        {
            _context = context;
        }

        public DataModels.OrderResponse MapToDataModel(LogicModels.OrderResponse model, DataModels.User creator)
        {
            var order = _context.Orders.Include(x => x.Creator.Profile).FirstOrDefault(x => x.Id == model.Order.Id);
            var provider = _context.Providers.Include(x => x.Creator.Profile)
                .FirstOrDefault(x => x.Id == model.Provider.Id);
            return new DataModels.OrderResponse
            {
                Id = model.Id,
                Order = order,
                Provider = provider,
                Creator = creator,
                Price = model.Price,
                Comment = model.Comment,
                IsCompleted = model.IsCompleted,
                CreatedAt = model.CreatedAt
            };
        }

        public LogicModels.OrderResponse MapToLogicModel(DataModels.OrderResponse model)
        {
            var provider = new ProviderMapper().MapToCommonModel(model.Provider);
            var order = new OrderMapper().MapToCommonModel(model.Order);
            var creator = new UserMapper().MapToCommonModel(model.Creator);
            return new LogicModels.OrderResponse
            {
                Id = model.Id,
                Order = order,
                Provider = provider,
                Creator = creator,
                Price = model.Price,
                Comment = model.Comment,
                IsCompleted = model.IsCompleted,
                CreatedAt = model.CreatedAt
            };
        }
    }
}
