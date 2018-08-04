using System.Linq;
using CommonModels = ServicePlace.Model;
using DataModels = ServicePlace.DataProvider.Entities;

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
            var user = new CommonModels.User();
            user.Id = model.Id;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.Password = model.PasswordHash;
            user.Name = model.Profile.Name;
            return user;
            //return new CommonModels.User
            //{
            //    Id = model.Id,
            //    UserName = model.UserName,
            //    Email = model.Email,
            //    Password = model.PasswordHash,
            //    Name = model.Profile.Name
            //};
        }
    }
}