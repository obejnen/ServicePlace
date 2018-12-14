using System.ComponentModel.DataAnnotations;

namespace ServicePlace.Model.ViewModels.OrderCategoryViewModels
{
    public class CreateOrderCategoryViewModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var model = obj as CreateOrderCategoryViewModel;
            return model != null &&
                   Name == model.Name;
        }
    }
}