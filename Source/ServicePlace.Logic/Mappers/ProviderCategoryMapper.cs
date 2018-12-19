using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Model.DataModels;
using ServicePlace.Model.ViewModels.ProviderCategoryViewModels;

namespace ServicePlace.Logic.Mappers
{
    public class ProviderCategoryMapper : IProviderCategoryMapper
    {
        public ProviderCategoryViewModel MapToProviderCategoryViewModel(ProviderCategory providerCategory)
        {
            return new ProviderCategoryViewModel
            {
                Id = providerCategory.Id,
                Name = providerCategory.Name
            };
        }

        public IEnumerable<SelectListItem> MapToSelectListItems(IEnumerable<ProviderCategory> providerCategories)
        {
            return providerCategories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
        }

        public IndexProviderCategoryViewModel MapToIndexProviderCategoryViewModel(
            IEnumerable<ProviderCategory> providerCategories)
        {
            return new IndexProviderCategoryViewModel
            {
                Categories = providerCategories.Select(MapToProviderCategoryViewModel)
            };
        }

        public ProviderCategory MapToProviderCategoryModel(CreateProviderCategoryViewModel createViewModel)
        {
            return new ProviderCategory
            {
                Id = createViewModel.Id,
                Name = createViewModel.Name
            };
        }

        public CreateProviderCategoryViewModel MapToCreateProviderCategoryViewModel(ProviderCategory providerCategory)
        {
            return new CreateProviderCategoryViewModel
            {
                Id = providerCategory.Id,
                Name = providerCategory.Name
            };
        }
    }
}
