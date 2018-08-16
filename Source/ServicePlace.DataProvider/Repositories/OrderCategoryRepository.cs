using System.Collections.Generic;
using System.Linq;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Interfaces;
using ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Repositories
{
    public class OrderCategoryRepository : IOrderCategoryRepository
    {
        private readonly ApplicationContext _context;

        public OrderCategoryRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Create(OrderCategory model)
        {
            _context.OrderCategories.Add(model);
            _context.SaveChanges();
        }

        public IEnumerable<OrderCategory> GetAll()
        {
            return _context.OrderCategories.ToList();
        }

        public OrderCategory FindById(int id)
        {
            return _context.OrderCategories.FirstOrDefault(x => x.Id == id);
        }
    }
}