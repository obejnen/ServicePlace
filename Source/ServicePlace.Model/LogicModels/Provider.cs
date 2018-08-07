using System;

namespace ServicePlace.Model.LogicModels
{
    public class Provider
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public decimal? Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public User Creator { get; set; }
    }
}
