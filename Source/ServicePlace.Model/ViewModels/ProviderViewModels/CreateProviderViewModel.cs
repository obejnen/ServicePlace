using System.ComponentModel.DataAnnotations;

namespace ServicePlace.Model.ViewModels.ProviderViewModels
{
    public class CreateProviderViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        public decimal? Price { get; set; }
    }
}