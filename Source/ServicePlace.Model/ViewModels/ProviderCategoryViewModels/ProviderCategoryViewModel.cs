namespace ServicePlace.Model.ViewModels.ProviderCategoryViewModels
{
    public class ProviderCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var model = obj as ProviderCategoryViewModel;
            return model != null &&
                   Id == model.Id &&
                   Name == model.Name;
        }
    }
}
