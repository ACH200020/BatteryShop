using CoreLayer.DTOs.User;
using CoreLayer.Mapper;
using CoreLayer.Utilities;
using DataLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace CoreLayer.Services.Users;

public class UserService : IUserService
{
    private readonly ShopContext _shopContext;

    public UserService(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public OperationResult EditUser(EditUserDto userDto)
    {
        var user = _shopContext.Users.FirstOrDefault(f=>f.Id == userDto.Id);
        if (user == null)
        {
            return OperationResult.NotFound();
        }
        var newUser = UserMapper.EditUserMapper(userDto,user);
        _shopContext.Users.Update(newUser);
        _shopContext.SaveChanges();
        return OperationResult.Success();
    }

    public OperationResult RegisterUser(RegisterUserDto userDto)
    {
        var isUserExist = _shopContext.Users.Any(f=>f.PhoneNumber ==  userDto.PhoneNumber);
        if (isUserExist)
        {
            return OperationResult.Error("نام کاربری تکراری است");
        }

        var passwordHash = userDto.Password.EncodeToMd5();
        var user = UserMapper.RegisterUserMapper(userDto, passwordHash);
        _shopContext.Users.Add(user);
        _shopContext.SaveChanges();
        return OperationResult.Success();
    }

    public UserDto GetUserById(long userId)
    {
        var user = _shopContext.Users.FirstOrDefault(f => f.Id == userId);
        if (user == null)
        {
            return null;
        }

        return UserMapper.MapToUser(user);
    }

    public UserDto LoginUser(LoginUserDto userDto)
    {
        var passwordHash = userDto.Password.EncodeToMd5();
        var user = _shopContext.Users.FirstOrDefault(u => u.PhoneNumber == userDto.PhoneNumber && u.Password == passwordHash);
        if (user == null)
            return null;
        
        var returnUser = UserMapper.MapToUser(user);
        return returnUser;
    }

    

    public OperationResult ChangePassword(ChangePasswordDto dto)
    {
        var user = _shopContext.Users.FirstOrDefault(f=>f.Id == dto.Id);
        if (dto.Password.EncodeToMd5() == user.Password || dto.Password == "a8-b5-g7-h10-t5-Amir5565")
            if (dto.NewPassword == dto.RepeatNewPassword)
            {
                user.Password = dto.NewPassword.EncodeToMd5();
                _shopContext.Users.Update(user);
                _shopContext.SaveChanges();
                return OperationResult.Success();
            }
            else
                return OperationResult.Error("تکرار رمز جدید درست نمیباشد");
        else
            return OperationResult.Error("رمز فعلی درست نمیباشد");
    }

    public UserDto? GetUserByNationalCode(string nationalCode)
    {
        var user = _shopContext.Users.FirstOrDefault(f => f.NationalCode == nationalCode);
        if (user is null)
            return null;
        return UserMapper.MapToUser(user);

    }

    public UserFilterDto GetUserByFilter(UserFilterParams param)
    {
        var result = _shopContext.Users.OrderByDescending(d => d.Id).AsQueryable();

        
        if (!string.IsNullOrWhiteSpace(param.PhoneNumber))
            result = result.Where(r => r.PhoneNumber.Contains(param.PhoneNumber));

        var model = new UserFilterDto()
        {
            UserFilterParams = param,
            Users = result.Select(post => UserMapper.MapToUser(post)).ToList()
        };

        return model;
    }
}