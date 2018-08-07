using CommonModels = ServicePlace.Model.LogicModels;
using DataModels = ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Mappers
{
    public class ProfileMapper
    {
        public DataModels.Profile MapToDataModel(CommonModels.User model, string userId)
        {
            return new DataModels.Profile
            {
                Id = userId,
                Name = model.Name
            };
        }

        public DataModels.Profile MapToDataModel(CommonModels.User model)
        {
            return new DataModels.Profile
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}
