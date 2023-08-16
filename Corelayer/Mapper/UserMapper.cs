using CoreLayer.DTOs.User;
using DataLayer.Entities.Users;

namespace CoreLayer.Mapper;

public class UserMapper
{
    public static User EditUserMapper(EditUserDto dto, User user)
    {

        user.Family = dto.Family;
        user.Id = dto.Id;
        user.Name = dto.Name;
        user.NationalCode = dto.NationalCode == null ? user.NationalCode : dto.NationalCode;
        user.PhoneNumber = dto.PhoneNumber == null ? user.PhoneNumber : dto.PhoneNumber;
        user.UserRole = dto.UserRole == null ? user.UserRole : dto.UserRole;
        return user;
    }

    public static User RegisterUserMapper(RegisterUserDto dto, string password)
    {
        return new User()
        {
            Family = dto.Family,
            Name = dto.Name,
            NationalCode = dto.NationalCode,
            PhoneNumber = dto.PhoneNumber,
            Password = password
        };
    }

    public static UserDto MapToUser(User user)
    {
        if (user == null)
        {
            return new UserDto();
        }

        return new UserDto()
        {
            Family = user.Family,
            Name = user.Name,
            NationalCode = user.NationalCode,
            PhoneNumber = user.PhoneNumber,
            Password = user.Password,
            Id = user.Id,
            UserRole = user.UserRole
        };
    }
}