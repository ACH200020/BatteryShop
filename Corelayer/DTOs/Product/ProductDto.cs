using CoreLayer.DTOs.Category;
using Microsoft.AspNetCore.Http;

namespace CoreLayer.DTOs.Product;

public class ProductDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ImageName { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public int? SubCategoryId { get; set; }
    public string Slug { get; set; }
    public string SeoData { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public int? Sales { get; set; }
    public int Ampere { get; set; }
    public string Brand { get; set; }
    public bool ShowProduct { get; set; }
    public CategoryDto Category { get; set; }
    public CategoryDto SubCategory { get; set; }

}

public class CreateProductDto
{
    public string Title { get; set; }
    public IFormFile ImageFile { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public int? SubCategoryId { get; set; }
    public string Slug { get; set; }
    public string SeoData { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public int? Sales { get; set; }
    public int Ampere { get; set; }
    public string Brand { get; set; }
    public bool ShowProduct { get; set; }


}
public class EditProductDto
{
    public int ProductId { get; set; }
    public string Title { get; set; }
    public IFormFile ImageFile { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public int? SubCategoryId { get; set; }
    public string Slug { get; set; }
    public string SeoData { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public int? Sales { get; set; }
    public int Ampere { get; set; }
    public string Brand { get; set; }
    public bool ShowProduct { get; set; }


}