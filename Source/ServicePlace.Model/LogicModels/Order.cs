using System;
using System.Collections.Generic;
using ServicePlace.Model.DataModels;

namespace ServicePlace.Model.LogicModels
{
    public class Order
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public User Creator { get; set; }
        public OrderCategory Category { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public bool Closed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
