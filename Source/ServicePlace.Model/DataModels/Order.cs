using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicePlace.Model.DataModels
{
    [Table(nameof(Order))]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public OrderCategory Category { get; set; }

        public bool Closed { get; set; }

        public bool Approved { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public User Creator { get; set; }

        public ICollection<Image> Images { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   Title == order.Title &&
                   Body == order.Body &&
                   EqualityComparer<OrderCategory>.Default.Equals(Category, order.Category) &&
                   Closed == order.Closed &&
                   Approved == order.Approved &&
                   EqualityComparer<User>.Default.Equals(Creator, order.Creator);
        }
    }
}