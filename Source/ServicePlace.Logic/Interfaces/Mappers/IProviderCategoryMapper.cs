using System.Collections.Generic;
using System.Web.Mvc;
using ServicePlace.Model.DataModels;
using ServicePlace.Model.ViewModels.ProviderCategoryViewModels;

namespace ServicePlace.Logic.Interfaces.Mappers
{
    public interface IProviderCategoryMapper
    {
        ProviderCategoryViewModel MapToProviderCategoryViewModel(ProviderCategory providerCategory);

        IEnumerable<SelectListItem> MapToSelectListItems(IEnumerable<ProviderCategory> providerCategories);

        IndexProviderCategoryViewModel MapToIndexProviderCategoryViewModel(
            IEnumerable<ProviderCategory> providerCategories);

        ProviderCategory MapToProviderCategoryModel(CreateProviderCategoryViewModel createViewModel);
    }
}
