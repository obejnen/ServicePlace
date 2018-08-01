using CommonModels = ServicePlace.Model;
using DataModels = ServicePlace.DataProvider.Models;

namespace ServicePlace.DataProvider.Mappers
{
    public class UserMapper
    {
        public DataModels.User MapToDataModel(CommonModels.User model)
        {
            return new DataModels.User
            {
                Id = model.Id,
                UserName = model.UserName,
                Email = model.Email,
                PasswordHash = model.PasswordHash
            };
        }

        public CommonModels.User MapToCommonModel(DataModels.User model)
        {
            return new CommonModels.User
            {
                Id = model.Id,
                UserName = model.UserName,
                Email = model.Email,
                PasswordHash = model.PasswordHash
            };
        }
    }
}