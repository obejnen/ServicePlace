using CommonModels = ServicePlace.Model.LogicModels;
using DataModels = ServicePlace.Model.DataModels;

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
