using System.Collections.Generic;

namespace ServicePlace.Model.ViewModels.OrderViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<ItemViewModel> FirstPart { get; set; }
        public IEnumerable<ItemViewModel> SecondPart { get; set; }
    }
}