using System.Linq;
using CommonModels = ServicePlace.Model.LogicModels;
using DataModels = ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Mappers
{
    public class OrderMapper
    {
        public DataModels.Order MapToDataModel(CommonModels.Order model, DataModels.User creator)
        {
            var imageMapper = new ImageMapper();
            var images = model.Images.Select(x => imageMapper.MapToDataModel(x)).ToList();
            return new DataModels.Order
            {
                Id = model.Id,
                Body = model.Body,
                Title = model.Title,
                Closed = model.Closed,
                Photos = images,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
                Creator = creator
            };
        }

        public CommonModels.Order MapToCommonModel(DataModels.Order model)
        {
            var imageMapper = new ImageMapper();
            var images = model.Photos.Select(x => imageMapper.MapToLogicModel(x));
            var creator = new UserMapper().MapToCommonModel(model.Creator);
            return new CommonModels.Order
            {
                Id = model.Id,
                Title = model.Title,
                Body = model.Body,
                Closed = model.Closed,
                Images = images,
                Creator = creator,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }
    }
}