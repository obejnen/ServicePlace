namespace ServicePlace.Model.ViewModels.OrderCategoryViewModels
{
    public class OrderCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is OrderCategoryViewModel model &&
                   Id == model.Id &&
                   Name == model.Name;
        }
    }
}