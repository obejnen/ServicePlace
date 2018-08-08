using System.Data.Entity;
using System.Linq;
using ServicePlace.DataProvider.DbContexts;
using DataModels = ServicePlace.Model.DataModels;
using LogicModels = ServicePlace.Model.LogicModels;

namespace ServicePlace.DataProvider.Mappers
{
    public class ProviderResponseMapper
    {
        private readonly ApplicationContext _context;

        public ProviderResponseMapper(ApplicationContext context)
        {
            _context = context;
        }

        public DataModels.ProviderResponse MapToDataModel(LogicModels.ProviderResponse model)
        {
            var order = _context.Orders.Include(x => x.Creator.Profile).FirstOrDefault(x => x.Id == model.Order.Id);
            var provider = _context.Providers.Include(x => x.Creator.Profile)
                .FirstOrDefault(x => x.Id == model.Provider.Id);

            return new DataModels.ProviderResponse
            {
                Id = model.Id,
                Order = order,
                Provider = provider,
                Comment = model.Comment,
                CreatedAt = model.CreatedAt
            };
        }

        public LogicModels.ProviderResponse MapToCommonModel(DataModels.ProviderResponse model)
        {
            var provider = new ProviderMapper().MapToCommonModel(model.Provider);
            var order = new OrderMapper().MapToCommonModel(model.Order);
            
            return new LogicModels.ProviderResponse
            {
                Id = model.Id,
                Order = order,
                Provider = provider,
                Comment = model.Comment,
                CreatedAt = model.CreatedAt
            };
        }
    }
}