using System;

namespace ServicePlace.Model.LogicModels
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Provider Provider { get; set; }
        public User Creator { get; set; }
        public decimal? Price { get; set; }
        public string Comment { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
