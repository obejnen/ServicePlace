using System;

namespace ServicePlace.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public User Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
