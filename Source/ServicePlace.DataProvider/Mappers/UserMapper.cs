using System.Linq;
using CommonModels = ServicePlace.Model.LogicModels;
using DataModels = ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Mappers
{
    public class UserMapper
    {
        public DataModels.User MapToDataModel(CommonModels.User model)
        {
            return new DataModels.User
            {
                UserName = model.UserName,
                Email = model.Email
            };
        }

        public CommonModels.User MapToCommonModel(DataModels.User model)
        {
            return new CommonModels.User
            {
                Id = model.Id,
                UserName = model.UserName,
                Email = model.Email,
                Password = model.PasswordHash,
                Name = model.Profile.Name
            };
        }
    }
}