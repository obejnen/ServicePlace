using CommonModels = ServicePlace.Model;
using DataModels = ServicePlace.DataProvider.Entities;

namespace ServicePlace.DataProvider.Mappers
{
    public class RoleMapper
    {
        public DataModels.Role MapToDataModel(CommonModels.Role role)
        {
            return new DataModels.Role
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public CommonModels.Role MapToCommonModel(DataModels.Role role)
        {
            return new CommonModels.Role
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}
