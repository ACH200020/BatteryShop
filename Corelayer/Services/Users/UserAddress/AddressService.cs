using CoreLayer.DTOs.User;
using CoreLayer.Mapper;
using CoreLayer.Utilities;
using DataLayer.Context;

namespace CoreLayer.Services.Users.UserAddress
{
    public class AddressService : IAddressService
    {
        private readonly ShopContext _shopContext;
        public AddressService(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public OperationResult AddUserAddress(AddUserAddressDto addressDto)
        {
            var address = AddressMapper.AddUserAddressMapper(addressDto);

            _shopContext.UserAddresses.Add(address);
            _shopContext.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult DeleteUserAddress(long addressId)
        {
            var address = _shopContext.UserAddresses.FirstOrDefault(x => x.Id == addressId);
            if (address == null)
                return OperationResult.NotFound();

            return OperationResult.Success();
        }

        public OperationResult EditUserAddress(EditUserAddressDto addressDto)
        {
            var address = _shopContext.UserAddresses.FirstOrDefault(x => x.Id == addressDto.Id);
            if (address == null)
                return OperationResult.NotFound();

            var newAddress = AddressMapper.EditUserAddressMapper(addressDto, address);

            _shopContext.UserAddresses.Update(newAddress);
            _shopContext.SaveChanges();
            return OperationResult.Success();
        }

        public UserAddressDto GetAddressById(long? Id)
        {
            var address = _shopContext.UserAddresses.FirstOrDefault(f=>f.Id == Id);
            if (address == null)
                return null;

            return new()
            {
                 ActiveAddress = address.ActiveAddress,
                 Address = address.Address,
                 City = address.City,
                 Id = (long)Id,
                 PostalCode = address.PostalCode,
                 Shire = address.Shire,

            };
        }

        public List<UserAddressDto> GetAddressByUserId(long userId)
        {
            return _shopContext.UserAddresses.Where(f => f.UserId == userId)
                .Select(f => AddressMapper.MapToDto(f))
                .ToList();
        }
    }
}
