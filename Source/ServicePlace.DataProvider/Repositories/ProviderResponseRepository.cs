using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.DataProvider.Mappers;
using ServicePlace.Model.LogicModels;

namespace ServicePlace.DataProvider.Repositories
{
    public class ProviderResponseRepository : IProviderResponseRepository
    {
        private readonly ApplicationContext _context;
        private readonly ProviderResponseMapper _mapper;

        public ProviderResponseRepository(ApplicationContext context)
        {
            _context = context;
            _mapper = new ProviderResponseMapper(_context);
        }

        public void Create(ProviderResponse model)
        {
            var providerResponse = _mapper.MapToDataModel(model);
            _context.ProviderResponses.Add(providerResponse);
            _context.SaveChanges();
        }

        public void Update(ProviderResponse model)
        {
            var providerResponse = _mapper.MapToDataModel(model);
            _context.ProviderResponses.AddOrUpdate(providerResponse);
            _context.SaveChanges();
        }

        public void Delete(ProviderResponse model)
        {
            var providerResponse = _mapper.MapToDataModel(model);
            _context.ProviderResponses.Remove(providerResponse);
            _context.SaveChanges();
        }

        public ProviderResponse FindById(object id)
        {
            return _mapper.MapToCommonModel(_context.ProviderResponses.FirstOrDefault(x => x.Id == (int) id));
        }

        public IEnumerable<ProviderResponse> GetAll()
        {
            return _context
                .ProviderResponses
                .Include(x => x.Order)
                .Include(x => x.Provider)
                .ToList()
                .Select(x => _mapper.MapToCommonModel(x));
        }

        public IEnumerable<ProviderResponse> GetProviderResponses(int providerId)
        {
            return _context
                .ProviderResponses
                .Include(x => x.Order)
                .Include(x => x.Provider)
                .Where(x => x.Provider.Id == providerId)
                .Select(x => _mapper.MapToCommonModel(x));
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
