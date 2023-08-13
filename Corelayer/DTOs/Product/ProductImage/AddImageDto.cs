using CoreLayer.Utilities;
using Microsoft.AspNetCore.Http;

namespace CoreLayer.DTOs.Product.ProductImage;

public class AddImageDto
{
    public int ProductId { get; set; }
    public IFormFile ImageFile { get; set; }
}

public class ImageDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ImageName { get; set; }
    public DataLayer.Entities.Products.Product Product { get; set; }
}

public class ImageFilterDto
{
    public List<ImageDto> ImageDtos { get; set; }
    public ImageFilterParams FilterParams { get; set; }

}

public class ImageFilterParams
{
    public string? ProductTitle { get; set; }
}

