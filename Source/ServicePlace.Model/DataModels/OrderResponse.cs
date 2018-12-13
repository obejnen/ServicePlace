using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicePlace.Model.DataModels
{
    [Table(nameof(OrderResponse))]
    public class OrderResponse
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Order Order { get; set; }

        [Required]
        public virtual Provider Provider { get; set; }

        public bool Completed { get; set; }

        public virtual User Creator { get; set; }

        public decimal? Price { get; set; }

        public string Comment { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public override bool Equals(object obj)
        {
            return obj is OrderResponse response &&
                   EqualityComparer<Order>.Default.Equals(Order, response.Order) &&
                   EqualityComparer<Provider>.Default.Equals(Provider, response.Provider) &&
                   Completed == response.Completed &&
                   EqualityComparer<User>.Default.Equals(Creator, response.Creator) &&
                   EqualityComparer<decimal?>.Default.Equals(Price, response.Price) &&
                   Comment == response.Comment &&
                   CreatedAt == response.CreatedAt;
        }
    }
}
