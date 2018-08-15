using CommonModels = ServicePlace.Model.LogicModels;
using DataModels = ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Mappers
{
    public class OrderMapper
    {
        public DataModels.Order MapToDataModel(CommonModels.Order model, DataModels.User creator)
        {
            return new DataModels.Order
            {
                Id = model.Id,
                Body = model.Body,
                Title = model.Title,
                Closed = model.Closed,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
                Creator = creator
            };
        }

        public CommonModels.Order MapToCommonModel(DataModels.Order model)
        {
            var creator = new UserMapper().MapToCommonModel(model.Creator);
            return new CommonModels.Order
            {
                Id = model.Id,
                Title = model.Title,
                Body = model.Body,
                Closed = model.Closed,
                Creator = creator,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }
    }
}