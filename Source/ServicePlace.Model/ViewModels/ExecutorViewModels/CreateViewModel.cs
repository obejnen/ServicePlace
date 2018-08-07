using System.ComponentModel.DataAnnotations;

namespace ServicePlace.Website.Models.ExecutorViewModels
{
    public class CreateViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        public decimal? Price { get; set; }
    }
}