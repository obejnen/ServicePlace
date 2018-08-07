using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.DataProvider.Mappers;
using ServicePlace.Model.LogicModels;


namespace ServicePlace.DataProvider.Repositories
{
    public class OrderResponseRepository : IOrderResponseRepository
    {
        private readonly ApplicationContext _context;
        private readonly OrderResponseMapper _mapper;

        public OrderResponseRepository(ApplicationContext context)
        {
            _context = context;
            _mapper = new OrderResponseMapper(_context);
        }

        public void Create(OrderResponse model)
        {
            var orderResponse = _mapper.MapToDataModel(model);
            _context.OrderResponses.Add(orderResponse);

            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                for(int i = 0; i < 10; i++) Debug.WriteLine("");
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                for (int i = 0; i < 10; i++) Debug.WriteLine("");

                throw;
            }

        }

        public void Update(OrderResponse model)
        {
            var orderResponse = _mapper.MapToDataModel(model);
            _context.OrderResponses.AddOrUpdate(orderResponse);
            _context.SaveChanges();
        }

        public void Delete(OrderResponse model)
        {
            var orderResponse = _mapper.MapToDataModel(model);
            _context.OrderResponses.Remove(orderResponse);
            _context.SaveChanges();
        }

        public OrderResponse FindById(object id)
        {
            return _mapper.MapToLogicModel(_context.OrderResponses.FirstOrDefault(x => x.Id == (int)id));
        }

        public IEnumerable<OrderResponse> GetAll()
        {
            return _context.OrderResponses.Include(x => x.Order).Include(x => x.Provider).ToList()
                .Select(x => _mapper.MapToLogicModel(x));
        }

        public IEnumerable<OrderResponse> GetOrderResponses(int id)
        {
            return _context.OrderResponses
                .Include(x => x.Order.Creator.Profile)
                .Include(x => x.Provider.Creator.Profile)
                .Where(x => x.Order.Id == id).ToList()
                .Select(x => _mapper.MapToLogicModel(x));
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
