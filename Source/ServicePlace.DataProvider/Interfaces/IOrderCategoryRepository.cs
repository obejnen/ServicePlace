using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IOrderCategoryRepository
    {
        void Create(OrderCategory model);

        IEnumerable<OrderCategory> GetAll();

        OrderCategory FindById(int id);
    }
}
