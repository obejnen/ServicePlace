using System.ComponentModel.DataAnnotations;

namespace ServicePlace.Model.ViewModels.ProviderCategoryViewModels
{
    public class CreateProviderCategoryViewModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}