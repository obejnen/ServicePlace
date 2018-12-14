using System.Collections.Generic;

namespace ServicePlace.Model.ViewModels.ProviderResponseViewModels
{
    public class IndexProviderResponseViewModel
    {
        public IEnumerable<ProviderResponseViewModel> ProviderResponses { get; set; }

        public override bool Equals(object obj)
        {
            var model = obj as IndexProviderResponseViewModel;
            return model != null &&
                   EqualityComparer<IEnumerable<ProviderResponseViewModel>>.Default.Equals(ProviderResponses, model.ProviderResponses);
        }
    }
}
