using System.Collections.Generic;

namespace ServicePlace.Model.ViewModels.OrderViewModels
{
    public class IndexOrderViewModel
    {
        public int CurrentPage { get; set; }
        public int MaxPage { get; set; }
        public int MinPage { get; set; }
        public IEnumerable<OrderViewModel> FirstPart { get; set; }
        public IEnumerable<OrderViewModel> SecondPart { get; set; }
        public IEnumerable<OrderViewModel> Orders { get; set; }
    }
}