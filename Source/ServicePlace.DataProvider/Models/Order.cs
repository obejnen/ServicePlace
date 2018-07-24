using System;
using System.ComponentModel.DataAnnotations;

namespace ServicePlace.DataProvider.Models
{
    public class Order
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public decimal Price { get; set; }

        public DateTime AimDate { get; set; }
        public int UserId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
