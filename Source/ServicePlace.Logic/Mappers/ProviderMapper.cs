﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ServicePlace.Common;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Logic.Interfaces.Services;
using ServicePlace.Model.DataModels;
using ServicePlace.Model.ViewModels.AccountViewModels;
using ServicePlace.Model.ViewModels.ProviderViewModels;

namespace ServicePlace.Logic.Mappers
{
    public class ProviderMapper : IProviderMapper
    {
        private readonly IProviderService _providerService;
        private readonly IProviderCategoryMapper _providerCategoryMapper;

        public ProviderMapper(IProviderService providerService, IProviderCategoryMapper providerCategoryMapper)
        {
            _providerService = providerService;
            _providerCategoryMapper = providerCategoryMapper;
        }

        public ProviderViewModel MapToProviderViewModel(Provider provider)
        {
            return new ProviderViewModel
            {
                Id = provider.Id,
                Title = provider.Title,
                Body = provider.Body,
                Price = provider.Price,
                Approved = provider.Approved,
                Images = provider.Images?.Select(x => x.Url),
                CreatedAt = provider.CreatedAt,
                User = new UserViewModel
                {
                    Id = provider.Creator.Id,
                    Avatar = provider.Creator.Avatar.Url,
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
                FirstPart = providerViewModels.Count() > Constants.ItemsPerRow
                    ? providerViewModels.Take(Constants.ItemsPerRow)
                    : providerViewModels.Take(providerViewModels.Count()),
                SecondPart = providerViewModels.Count() > Constants.ItemsPerRow
                    ? providerViewModels.Skip(Constants.ItemsPerRow)
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

        public CreateProviderViewModel GetCreateProviderViewModel()
        {
            return new CreateProviderViewModel
            {
                Categories = _providerCategoryMapper.MapToSelectListItems(_providerService.GetCategories())
            };
        }

        public CreateProviderViewModel MapToCreateProviderViewModel(Provider provider)
        {
            return new CreateProviderViewModel
            {
                Id = provider.Id,
                Title = provider.Title,
                Body = provider.Body,
                Price = provider.Price,
                Categories = _providerCategoryMapper.MapToSelectListItems(_providerService.GetCategories()),
                CategoryId = provider.Category.Id,
                CreatedAt = provider.CreatedAt
            };
        }

        public Provider MapToProviderModel(CreateProviderViewModel createViewModel, User creator)
        {
            return new Provider
            {
                Id = createViewModel.Id,
                Title = createViewModel.Title,
                Body = createViewModel.Body,
                Creator = creator,
                Price = createViewModel.Price,
                Category = _providerService.GetCategory(createViewModel.CategoryId),
                Images = createViewModel.Images?.Trim().Split(' ').Select(image => new Image { Url = image }).ToList()
            };
        }
    }
}
