using System.Collections.Generic;
using ServicePlace.Model.DataModels;
using ServicePlace.Model.ViewModels.ProviderResponseViewModels;

namespace ServicePlace.Logic.Interfaces.Mappers
{
    public interface IProviderResponseMapper
    {
        ProviderResponseViewModel MapToProviderResponseViewModel(ProviderResponse providerResponse);

        IndexProviderResponseViewModel MapToIndexProviderResponseViewModel(
            IEnumerable<ProviderResponse> providerResponses);

        CreateProviderResponseViewModel GetCreateProviderResponseViewModel(string userId, int providerId);

        ProviderResponse MapToProviderResponseModel(CreateProviderResponseViewModel createViewModel, User creator);
    }
}