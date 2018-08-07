using CommonModels = ServicePlace.Model.LogicModels;
using DataModels = ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Mappers
{
    public class ExecutorMapper
    {
        public DataModels.Executor MapToDataModel(CommonModels.Executor model, DataModels.User creator)
        {
            return new DataModels.Executor
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

        public CommonModels.Executor MapToCommonModel(DataModels.Executor model)
        {
            var creator = new UserMapper().MapToCommonModel(model.Creator);
            return new CommonModels.Executor
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