using CoreLayer.DTOs.Category;
using Microsoft.AspNetCore.Http;

namespace CoreLayer.DTOs.Product;

public class ProductDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string ImageName { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public long? SubCategoryId { get; set; }
    public string Slug { get; set; }
    public string SeoData { get; set; }
    public int Count { get; set; }
    public long Price { get; set; }
    public int? Sales { get; set; }
    public int Ampere { get; set; }
    public string Brand { get; set; }
    public CategoryDto Category { get; set; }
    public CategoryDto SubCategory { get; set; }

}

public class CreateProductDto
{
    public string Title { get; set; }
    public IFormFile ImageFile { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public long? SubCategoryId { get; set; }
    public string Slug { get; set; }
    public string SeoData { get; set; }
    public int Count { get; set; }
    public long Price { get; set; }
    public int? Sales { get; set; }
    public int Ampere { get; set; }
    public string Brand { get; set; }

}
public class EditProductDto
{
    public long ProductId { get; set; }
    public string Title { get; set; }
    public IFormFile ImageFile { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public long? SubCategoryId { get; set; }
    public string Slug { get; set; }
    public string SeoData { get; set; }
    public int Count { get; set; }
    public long Price { get; set; }
    public int? Sales { get; set; }
    public int Ampere { get; set; }
    public string Brand { get; set; }

}