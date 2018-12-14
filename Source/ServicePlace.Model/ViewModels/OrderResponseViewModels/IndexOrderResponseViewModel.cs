using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePlace.Model.ViewModels.OrderResponseViewModels
{
    public class IndexOrderResponseViewModel
    {
        public IEnumerable<OrderResponseViewModel> OrderResponses { get; set; }

        public override bool Equals(object obj)
        {
            return obj is IndexOrderResponseViewModel model &&
                   EqualityComparer<IEnumerable<OrderResponseViewModel>>.Default.Equals(OrderResponses, model.OrderResponses);
        }
    }
}
