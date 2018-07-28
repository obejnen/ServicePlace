using System.ComponentModel.DataAnnotations;

namespace ServicePlace.Website.Models.OrderViewModels
{
    public class CreateViewModel
    {
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Body { get; set; }
    }
}