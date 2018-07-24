using System;
using System.ComponentModel.DataAnnotations;

namespace ServicePlace.DataProvider.Models
{
    public class Executor
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        public decimal Price { get; set; }

        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
