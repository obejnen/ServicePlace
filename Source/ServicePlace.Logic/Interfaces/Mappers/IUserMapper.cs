using ServicePlace.Model.DataModels;
using ServicePlace.Model.DTOModels;
using ServicePlace.Model.ViewModels;
using ServicePlace.Model.ViewModels.AccountViewModels;

namespace ServicePlace.Logic.Interfaces.Mappers
{
    public interface IUserMapper
    {
        ProfileViewModel MapToProfileViewModel(User user);

        UserDTO MapToUserDtoModel(RegisterViewModel registerViewModel);

        UserDTO MapToUserDtoModel(User user);
    }
}