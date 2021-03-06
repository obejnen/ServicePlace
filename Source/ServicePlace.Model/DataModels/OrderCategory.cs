﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicePlace.Model.DataModels
{
    [Table(nameof(OrderCategory))]
    public class OrderCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }

        public override bool Equals(object obj)
        {
            return obj is OrderCategory category &&
                   Name == category.Name &&
                   EqualityComparer<ICollection<Order>>.Default.Equals(Orders, category.Orders);
        }
    }
}