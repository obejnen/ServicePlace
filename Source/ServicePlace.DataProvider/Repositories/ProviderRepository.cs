using AutoMapper;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using CommonModels = ServicePlace.Model.LogicModels;
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

        public IEnumerable<CommonModels.Provider> GetAll()
        {
            var result = _context.Executors.Include(x => x.Creator.Profile).ToList()
                .Select(x => _mapper.MapToCommonModel(x));
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<List<CommonModels.Provider>, IEnumerable<CommonModels.Provider>>());
            return Mapper.Map<IEnumerable<CommonModels.Provider>>(result);
        }

        public void Create(CommonModels.Provider model)
        {
            var creator = _context.Users.FirstOrDefault(x => x.Id == model.Creator.Id);
            var executor = _mapper.MapToDataModel(model, creator);
            _context.Executors.Add(executor);
            _context.SaveChanges();
        }

        public void Delete(CommonModels.Provider model)
        {
            var creator = _context.Users.FirstOrDefault(x => x.Id == model.Creator.Id);
            var executor = _mapper.MapToDataModel(model, creator);
            _context.Executors.Remove(executor);
            _context.SaveChanges();
        }

        public void Update(CommonModels.Provider model)
        {
            var creator = _context.Users.FirstOrDefault(x => x.Id == model.Creator.Id);
            var newExecutor = _mapper.MapToDataModel(model, creator);
            _context.Executors.AddOrUpdate(newExecutor);
            _context.SaveChanges();
        }

        public CommonModels.Provider FindById(object id)
        {
            var executor = _context.Executors.Include(x => x.Creator.Profile).FirstOrDefault(x => x.Id == (int) id);
            return _mapper.MapToCommonModel(executor);
        }

        public IEnumerable<CommonModels.Provider> Search(string search)
        {
            var executors = _context.Executors.Where(x => x.Title.Contains(search) || x.Body.Contains(search));
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<List<CommonModels.Provider>, IEnumerable<CommonModels.Provider>>());
            return Mapper.Map<IEnumerable<CommonModels.Provider>>(executors.Select(x => _mapper.MapToCommonModel(x)));
        }

        public IEnumerable<CommonModels.Provider> Take(int skip, int count)
        {
            var executors = _context.Executors.Skip(skip).Take(count);
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<List<CommonModels.Provider>, IEnumerable<CommonModels.Provider>>());
            return Mapper.Map<IEnumerable<CommonModels.Provider>>(executors.Select(x => _mapper.MapToCommonModel(x)));
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}