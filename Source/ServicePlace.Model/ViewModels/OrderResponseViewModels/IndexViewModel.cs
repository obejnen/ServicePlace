using System;

namespace ServicePlace.Model.ViewModels.OrderResponseViewModels
{
    public class IndexViewModel
    {
        public int Id { get; set; }
        public ProviderViewModels.IndexViewModel Provider { get; set; }
        public OrderViewModels.IndexViewModel Order { get; set; }
        public decimal? Price { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Completed { get; set; }
    }
}
