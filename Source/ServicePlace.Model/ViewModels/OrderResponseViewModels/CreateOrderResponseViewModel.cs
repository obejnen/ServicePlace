using System.Collections.Generic;
using System.Web.Mvc;

namespace ServicePlace.Model.ViewModels.OrderResponseViewModels
{
    public class CreateOrderResponseViewModel
    {
        public int OrderId { get; set; }
        public int ProviderId { get; set; }
        public decimal? Price { get; set; }
        public string Comment { get; set; }
        public IEnumerable<SelectListItem> Providers { get; set; }
    }
}