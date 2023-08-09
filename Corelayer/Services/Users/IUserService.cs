using CoreLayer.DTOs.User;
using CoreLayer.Utilities;

namespace CoreLayer.Services.Users;

public interface IUserService
{
    OperationResult EditUser(EditUserDto userDto);
    OperationResult RegisterUser(RegisterUserDto userDto);
    UserDto GetUserById(long userId);  
    UserDto LoginUser(LoginUserDto userDto);



    OperationResult ChangePassword(ChangePasswordDto dto);

    UserDto? GetUserByNationalCode(string nationalCode);

    UserFilterDto GetUserByFilter(UserFilterParams param);
}