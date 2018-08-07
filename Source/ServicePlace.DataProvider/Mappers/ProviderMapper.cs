using CommonModels = ServicePlace.Model.LogicModels;
using DataModels = ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Mappers
{
    public class ProviderMapper
    {
        public DataModels.Provider MapToDataModel(CommonModels.Provider model, DataModels.User creator)
        {
            return new DataModels.Provider
            {
                Id = model.Id,
                Body = model.Body,
                Title = model.Title,
                Price = model.Price,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
                Creator = creator
            };
        }

        public CommonModels.Provider MapToCommonModel(DataModels.Provider model)
        {
            var creator = new UserMapper().MapToCommonModel(model.Creator);
            return new CommonModels.Provider
            {
                Id = model.Id,
                Title = model.Title,
                Body = model.Body,
                Price = model.Price,
                Creator = creator,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }
    }
}