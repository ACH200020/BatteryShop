using CoreLayer.DTOs.User;
using CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services.Users.UserAddress
{
    public interface IAddressService
    {

        List<UserAddressDto> GetAddressByUserId(long userId);

        OperationResult AddUserAddress(AddUserAddressDto addressDto);

        OperationResult EditUserAddress(EditUserAddressDto addressDto);

        OperationResult DeleteUserAddress(long addressId);

        UserAddressDto GetAddressById(long? Id);
    }
}
