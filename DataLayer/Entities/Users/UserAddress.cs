using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Users;

public class UserAddress : BaseEntities
{
    public long UserId { get; set; }
    public string Shire { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Address { get; set; }
    public bool ActiveAddress { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
}