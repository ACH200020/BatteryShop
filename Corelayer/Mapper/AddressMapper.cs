using CoreLayer.DTOs.User;
using DataLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Mapper
{
    public class AddressMapper
    {
        public static UserAddress AddUserAddressMapper(AddUserAddressDto dto)
        {
            return new UserAddress()
            {
                ActiveAddress = dto.ActiveAddress,
                City = dto.City,
                PostalCode = dto.PostalCode,
                Shire = dto.Shire,
                UserId = dto.UserId,
                Address = dto.Address,
            };
        }

        public static UserAddress EditUserAddressMapper(EditUserAddressDto dto, UserAddress address)
        {
            address.Id = dto.Id;
            address.ActiveAddress = dto.ActiveAddress;
            address.City = dto.City;
            address.PostalCode = dto.PostalCode;
            address.Shire = dto.Shire;
            address.Address = dto.Address;

            return address;
        }

        public static UserAddressDto MapToDto(UserAddress address)
        {
            return new UserAddressDto()
            {
                ActiveAddress = address.ActiveAddress,
                City = address.City,
                PostalCode = address.PostalCode,
                Shire = address.Shire,
                Address = address.Address,
                Id = address.Id

            };
        }
    }
}
