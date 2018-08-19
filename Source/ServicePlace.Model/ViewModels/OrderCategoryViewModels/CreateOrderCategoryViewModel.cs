using System.ComponentModel.DataAnnotations;

namespace ServicePlace.Model.ViewModels.OrderCategoryViewModels
{
    public class CreateOrderCategoryViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}