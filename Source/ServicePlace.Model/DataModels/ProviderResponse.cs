﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicePlace.Model.DataModels
{
    [Table(nameof(ProviderResponse))]
    public class ProviderResponse
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Order Order { get; set; }

        [Required]
        public Provider Provider { get; set; }

        public virtual User Creator { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ProviderResponse response &&
                   EqualityComparer<Order>.Default.Equals(Order, response.Order) &&
                   EqualityComparer<Provider>.Default.Equals(Provider, response.Provider) &&
                   EqualityComparer<User>.Default.Equals(Creator, response.Creator) &&
                   Comment == response.Comment &&
                   CreatedAt == response.CreatedAt;
        }
    }
}