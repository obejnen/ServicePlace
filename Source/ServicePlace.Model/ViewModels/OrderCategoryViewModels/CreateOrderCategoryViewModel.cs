using System.ComponentModel.DataAnnotations;

namespace ServicePlace.Model.ViewModels.OrderCategoryViewModels
{
    public class CreateOrderCategoryViewModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}