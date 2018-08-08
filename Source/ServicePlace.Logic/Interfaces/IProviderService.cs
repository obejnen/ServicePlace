﻿using System.Collections.Generic;
using ServicePlace.Model.LogicModels;

namespace ServicePlace.Logic.Interfaces
{
    public interface IProviderService
    {
        IEnumerable<Provider> Providers { get; }

        void Create(Provider provider);

        void Delete(Provider provider);

        void Update(Provider provider);

        Provider FindById(object id);

        IEnumerable<Provider> Search(string query);

        IEnumerable<Provider> Take(int skip, int count);

        IEnumerable<Provider> GetUserProviders(User user);

        void CreateResponse(ProviderResponse response);

        IEnumerable<ProviderResponse> GetProviderResponses(int providerId);
    }
}