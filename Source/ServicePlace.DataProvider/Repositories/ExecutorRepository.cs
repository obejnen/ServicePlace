using AutoMapper;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using ServicePlace.Common.Enums;
using CommonModels = ServicePlace.Model;
using ServicePlace.DataProvider.Mappers;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Interfaces;

namespace ServicePlace.DataProvider.Repositories
{
    public class ExecutorRepository : IExecutorRepository<CommonModels.Executor, int, ResponseType>
    {
        private readonly ApplicationContext _context;
        private readonly ExecutorMapper _mapper;

        public ExecutorRepository(ApplicationContext context)
        {
            _context = context;
            _mapper = new ExecutorMapper();
        }

        public IEnumerable<CommonModels.Executor> GetAll()
        {
            var result = _context.Executors.Include(x => x.Creator.Profile).ToList()
                .Select(x => _mapper.MapToCommonModel(x));
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<List<CommonModels.Executor>, IEnumerable<CommonModels.Executor>>());
            return Mapper.Map<IEnumerable<CommonModels.Executor>>(result);
        }

        public ResponseType Create(CommonModels.Executor model)
        {
            var creator = _context.Users.FirstOrDefault(x => x.Id == model.Creator.Id);
            var executor = _mapper.MapToDataModel(model, creator);
            _context.Executors.Add(executor);
            return _context.SaveChanges() > 0
                ? ResponseType.Success
                : ResponseType.Failed;
        }

        public ResponseType Delete(CommonModels.Executor model)
        {
            var creator = _context.Users.FirstOrDefault(x => x.Id == model.Creator.Id);
            var executor = _mapper.MapToDataModel(model, creator);
            _context.Executors.Remove(executor);
            return _context.SaveChanges() > 0
                ? ResponseType.Success
                : ResponseType.Failed;
        }

        public ResponseType Update(CommonModels.Executor model)
        {
            var creator = _context.Users.FirstOrDefault(x => x.Id == model.Creator.Id);
            var newExecutor = _mapper.MapToDataModel(model, creator);
            _context.Executors.AddOrUpdate(newExecutor);
            return _context.SaveChanges() > 0
                ? ResponseType.Success
                : ResponseType.Failed;
        }

        public CommonModels.Executor FindById(int id)
        {
            var executor = _context.Executors.SingleOrDefault(x => x.Id == id);
            return _mapper.MapToCommonModel(executor);
        }

        public IEnumerable<CommonModels.Executor> Search(string search)
        {
            var executors = _context.Executors.Where(x => x.Title.Contains(search) || x.Body.Contains(search));
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<List<CommonModels.Executor>, IEnumerable<CommonModels.Executor>>());
            return Mapper.Map<IEnumerable<CommonModels.Executor>>(executors.Select(x => _mapper.MapToCommonModel(x)));
        }

        public IEnumerable<CommonModels.Executor> Take(int skip, int count)
        {
            var executors = _context.Executors.Skip(skip).Take(count);
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<List<CommonModels.Executor>, IEnumerable<CommonModels.Executor>>());
            return Mapper.Map<IEnumerable<CommonModels.Executor>>(executors.Select(x => _mapper.MapToCommonModel(x)));
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}