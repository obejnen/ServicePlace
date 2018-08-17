using System.Collections.Generic;
using System.Web.Mvc;
using ServicePlace.Model.DataModels;
using ServicePlace.Model.ViewModels.ProviderViewModels;

namespace ServicePlace.Logic.Interfaces.Mappers
{
    public interface IProviderMapper
    {
        ProviderViewModel MapToProviderViewModel(Provider provider);

        IndexProviderViewModel MapToIndexProviderViewModel(IEnumerable<Provider> providers, int[] pages);

        IndexProviderViewModel MapToIndexProviderViewModel(IEnumerable<Provider> providers);

        IEnumerable<SelectListItem> MapToSelectListItems(IEnumerable<Provider> providers);

        Provider MapToProviderModel(CreateProviderViewModel createProviderViewModel, User creator);
    }
}
