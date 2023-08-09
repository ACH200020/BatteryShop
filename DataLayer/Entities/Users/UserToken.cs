using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Users;

public class UserToken : BaseEntities
{
    public long UserId { get; set; }
    public string HashJwtToken { get; set; }
    public string HashRefreshToken { get; set; }
    public DateTime TokenExpireDate { get; set; }
    public DateTime RefreshTokenExpireDate { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
}