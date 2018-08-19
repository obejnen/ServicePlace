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

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public User Creator { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}