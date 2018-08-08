using System;
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

        [Required]
        public string Comment { get; set; }

        [Required]
        public DateTime? CreatedAt { get; set; }
    }
}