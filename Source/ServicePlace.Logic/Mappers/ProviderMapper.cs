using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Model.DataModels;
using ServicePlace.Model.ViewModels.AccountViewModels;
using ServicePlace.Model.ViewModels.ProviderViewModels;

namespace ServicePlace.Logic.Mappers
{
    public class ProviderMapper : IProviderMapper
    {
        public ProviderViewModel MapToProviderViewModel(Provider provider)
        {
            return new ProviderViewModel
            {
                Id = provider.Id,
                Title = provider.Title,
                Body = provider.Body,
                Price = provider.Price,
                CreatedAt = provider.CreatedAt,
                UpdatedAt = provider.UpdatedAt,
                User = new UserViewModel
                {
                    Id = provider.Creator.Id,
                    UserName = provider.Creator.UserName,
                    Name = provider.Creator.Profile.Name
                }
            };
        }

        public IndexProviderViewModel MapToIndexProviderViewModel(IEnumerable<Provider> providers, int[] pages)
        {
            var providerViewModels = providers.Select(MapToProviderViewModel).ToList();
            return new IndexProviderViewModel
            {
                CurrentPage = pages[0],
                MinPage = pages[1],
                MaxPage = pages[2],
                FirstPart = providerViewModels.Count() > 4
                    ? providerViewModels.Take(4)
                    : providerViewModels.Take(providerViewModels.Count()),
                SecondPart = providerViewModels.Count() > 4
                    ? providerViewModels.Skip(4)
                    : null
            };
        }

        public IndexProviderViewModel MapToIndexProviderViewModel(IEnumerable<Provider> providers)
        {
            return new IndexProviderViewModel
            {
                Providers = providers.Select(MapToProviderViewModel)
            };
        }

        public IEnumerable<SelectListItem> MapToSelectListItems(IEnumerable<Provider> providers)
        {
            return providers.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Title
            });
        }

        public Provider MapToProviderModel(CreateProviderViewModel createViewModel, User creator)
        {
            return new Provider
            {
                Title = createViewModel.Title,
                Body = createViewModel.Body,
                Creator = creator,
                Price = createViewModel.Price
            };
        }
    }
}
