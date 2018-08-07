using System.ComponentModel.DataAnnotations;

namespace ServicePlace.Model.ViewModels.ProviderViewModels
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