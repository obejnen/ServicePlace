﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicePlace.Model.DataModels
{
    [Table(nameof(ProviderCategory))]
    public class ProviderCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Provider> Providers { get; set; }
    }
}