namespace CoreLayer.DTOs.User;

public class AddUserAddressDto
{
    public long UserId { get; set; }
    public string Shire { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public bool ActiveAddress { get; set; }
    public string Address { get; set; }
}

public class EditUserAddressDto
{
    public long Id { get; set; }
    public string Shire { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public bool ActiveAddress { get; set; }
    public string Address { get; set; }

}

public class UserAddressDto
{
    public long Id { get; set; }
    public string Shire { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public bool ActiveAddress { get; set; }
    public string Address { get; set; }


}
