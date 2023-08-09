using CoreLayer.Utilities;
using DataLayer.Entities.Users;

namespace CoreLayer.DTOs.User;

public class LoginUserDto
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }

}


public class ChangePasswordDto
{
    public string Password { get; set; }
    public string NewPassword { get; set; }
    public string RepeatNewPassword { get; set; }
    public long Id { get; set; }
}


public class RegisterUserDto
{
    public string Name { get; set; }
    public string Family { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string NationalCode { get; set; }
}

public class UserDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Family { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string NationalCode { get; set; }
    public UserRole UserRole { get; set; }
}
public class EditUserDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Family { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string NationalCode { get; set; }
    public UserRole UserRole { get; set; }
}



public class UserFilterDto : BasePagination
{
    public List<UserDto> Users { get; set; }
    public UserFilterParams UserFilterParams { get; set; }
}

public class UserFilterParams
{
    public string PhoneNumber { get; set; }
}
