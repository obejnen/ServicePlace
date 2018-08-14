using System;

namespace ServicePlace.Model.LogicModels
{
    public class ProviderResponse
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Provider Provider { get; set; }
        public User Creator { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}