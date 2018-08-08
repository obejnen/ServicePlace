using System;

namespace ServicePlace.Model.ViewModels.ProviderResponseViewModels
{
    public class IndexViewModel
    {
        public OrderViewModels.IndexViewModel Order { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
