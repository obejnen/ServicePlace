using DataModels = ServicePlace.Model.DataModels;
using LogicModels = ServicePlace.Model.LogicModels;

namespace ServicePlace.DataProvider.Mappers
{
    public class ImageMapper
    {
        public DataModels.Image MapToDataModel(LogicModels.Image model)
        {
            return new DataModels.Image
            {
                Id = model.Id,
                Url = model.Url
            };
        }

        public LogicModels.Image MapToLogicModel(DataModels.Image model)
        {
            return new LogicModels.Image
            {
                Id = model.Id,
                Url = model.Url
            };
        }
    }
}