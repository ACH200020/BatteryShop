namespace BatteryShop_Web.Areas.Admin.Models.ProductImage;

public class AddProductImageViewModel
{
    public IFormFile ImageFile { get; set; }
    public int ProductId { get; set; }
}