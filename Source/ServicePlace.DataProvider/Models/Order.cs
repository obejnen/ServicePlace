using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicePlace.DataProvider.Models
{
    [Table("Order")]
    public class Order
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string CreatorId { get; set; }

        [Required]
        [MaxLength(256)]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [ForeignKey(nameof(CreatorId))]
        public virtual User Creator { get; set; }
    }
}
