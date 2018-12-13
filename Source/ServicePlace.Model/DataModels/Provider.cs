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

        public bool Approved { get; set; }

        [Required]
        public User Creator { get; set; }

        public ICollection<Image> Images { get; set; }

        public ICollection<OrderResponse> OrderResponses { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Provider provider &&
                   Title == provider.Title &&
                   Body == provider.Body &&
                   EqualityComparer<decimal?>.Default.Equals(Price, provider.Price) &&
                   EqualityComparer<ProviderCategory>.Default.Equals(Category, provider.Category) &&
                   CreatedAt == provider.CreatedAt &&
                   Approved == provider.Approved &&
                   EqualityComparer<User>.Default.Equals(Creator, provider.Creator) &&
                   EqualityComparer<ICollection<Image>>.Default.Equals(Images, provider.Images) &&
                   EqualityComparer<ICollection<OrderResponse>>.Default.Equals(OrderResponses, provider.OrderResponses);
        }
    }
}