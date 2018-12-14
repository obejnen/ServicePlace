using System.Collections.Generic;

namespace ServicePlace.Model.ViewModels.ProviderViewModels
{
    public class IndexProviderViewModel
    {
        public int CurrentPage { get; set; }
        public int MaxPage { get; set; }
        public int MinPage { get; set; }
        public IEnumerable<ProviderViewModel> FirstPart { get; set; }
        public IEnumerable<ProviderViewModel> SecondPart { get; set; }
        public IEnumerable<ProviderViewModel> Providers { get; set; }

        public override bool Equals(object obj)
        {
            var model = obj as IndexProviderViewModel;
            return model != null &&
                   CurrentPage == model.CurrentPage &&
                   MaxPage == model.MaxPage &&
                   MinPage == model.MinPage &&
                   EqualityComparer<IEnumerable<ProviderViewModel>>.Default.Equals(FirstPart, model.FirstPart) &&
                   EqualityComparer<IEnumerable<ProviderViewModel>>.Default.Equals(SecondPart, model.SecondPart) &&
                   EqualityComparer<IEnumerable<ProviderViewModel>>.Default.Equals(Providers, model.Providers);
        }
    }
}