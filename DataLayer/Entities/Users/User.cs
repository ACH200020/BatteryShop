using DataLayer.Entities.Comments;
using DataLayer.Entities.Orders;

namespace DataLayer.Entities.Users
{
    public class User : BaseEntities
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string NationalCode { get; set; }
        public UserRole UserRole { get; set; } = (UserRole)2;



        public List<Order> Orders { get; set; }
        public List<UserAddress> UserAddresses { get; set; }
        public List<Comment> Comments { get; set; }
        public List<OrderFinally> OrderFinallies { get; set; }
        
    }

    public enum UserRole
    {
        Admin,
        Owen,
        User
    }
}