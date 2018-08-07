using System;
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
        public Provider Provider { get; set; }

        public decimal? Price { get; set; }

        public string Comment { get; set; }

        public bool IsCompleted { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
