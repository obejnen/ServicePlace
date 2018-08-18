using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicePlace.Model.DataModels
{
    [Table(nameof(Provider))]
    public class Provider
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        public decimal? Price { get; set; }

        [Required]
        public ProviderCategory Category { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [Required]
        public User Creator { get; set; }

        public ICollection<Image> Images { get; set; }

        public ICollection<OrderResponse> OrderResponses { get; set; }
    }
}