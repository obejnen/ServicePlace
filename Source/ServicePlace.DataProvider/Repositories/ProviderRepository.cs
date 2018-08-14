using AutoMapper;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using ServicePlace.Model.LogicModels;
using ServicePlace.DataProvider.Mappers;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Interfaces;

namespace ServicePlace.DataProvider.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly ApplicationContext _context;
        private readonly ProviderMapper _mapper;

        public ProviderRepository(ApplicationContext context)
        {
            _context = context;
            _mapper = new ProviderMapper();
        }

        public IEnumerable<Provider> GetAll()
        {
            var result = _context.Providers.Include(x => x.Creator.Profile).ToList()
                .Select(x => _mapper.MapToCommonModel(x));
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<List<Provider>, IEnumerable<Provider>>());
            return Mapper.Map<IEnumerable<Provider>>(result);
        }

        public void Create(Provider model)
        {
            var creator = _context.Users.FirstOrDefault(x => x.Id == model.Creator.Id);
            var executor = _mapper.MapToDataModel(model, creator);
            _context.Providers.Add(executor);
            _context.SaveChanges();
        }

        public void Delete(Provider model)
        {
            var creator = _context.Users.FirstOrDefault(x => x.Id == model.Creator.Id);
            var executor = _mapper.MapToDataModel(model, creator);
            _context.Providers.Remove(executor);
            _context.SaveChanges();
        }

        public void Update(Provider model)
        {
            var creator = _context.Users.FirstOrDefault(x => x.Id == model.Creator.Id);
            var newExecutor = _mapper.MapToDataModel(model, creator);
            _context.Providers.AddOrUpdate(newExecutor);
            _context.SaveChanges();
        }

        public Provider FindById(object id)
        {
            var executor = _context.Providers.Include(x => x.Creator.Profile).FirstOrDefault(x => x.Id == (int) id);
            return _mapper.MapToCommonModel(executor);
        }

        public IEnumerable<Provider> Search(string search)
        {
            var providers = _context.Providers.Include(x => x.Creator.Profile).Where(x => x.Title.Contains(search) || x.Body.Contains(search)).ToList();
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<List<Provider>, IEnumerable<Provider>>());
            return Mapper.Map<IEnumerable<Provider>>(providers.Select(x => _mapper.MapToCommonModel(x)));
        }

        public IEnumerable<Provider> Take(int skip, int count)
        {
            var executors = _context.Providers.Include(x => x.Creator.Profile).OrderBy(x => x.CreatedAt).Skip(skip).Take(count).ToList();
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<List<Provider>, IEnumerable<Provider>>());
            return Mapper.Map<IEnumerable<Provider>>(executors.Select(x => _mapper.MapToCommonModel(x)));
        }

        public int GetProvidersCount() => _context.Providers.Count();

        public IEnumerable<Provider> GetUserProviders(string userId)
        {
            return _context
                .Providers
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