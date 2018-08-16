using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicePlace.Model.DataModels
{
    [Table(nameof(Image))]
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }
    }
}