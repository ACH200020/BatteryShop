using CoreLayer.DTOs.Product;
using CoreLayer.Utilities;
using DataLayer.Entities.Categories;
using DataLayer.Entities.Products;

namespace CoreLayer.Mapper;

public class ProductMapper
{
    public static Product MapCreateDtoToProduct(CreateProductDto dto)
    {
        return new Product()
        {
            CategoryId = dto.CategoryId,
            Description = dto.Description,
            Slug = dto.Slug.ToSlug(),
            Count = dto.Count,
            SeoData = dto.SeoData,
            SubCategoryId = dto.SubCategoryId,
            Title = dto.Title,
            Price = dto.Price,
            Sales = dto.Sales,
            Ampere = dto.Ampere,
            Brand = dto.Brand

        };
    }

    public static Product EditProduct(EditProductDto dto, Product product)
    {
        product.CategoryId = dto.CategoryId;
        product.Description = dto.Description;
        product.Slug = dto.Slug;
        product.Count = dto.Count;
        product.SeoData = dto.SeoData;
        product.SubCategoryId = dto.SubCategoryId;
        product.Title = dto.Title;
        product.Price = dto.Price;
        product.Sales = dto.Sales;
        product.Ampere = dto.Ampere;
        product.Brand = dto.Brand;
        return product;
    }


    public static ProductDto MapToDto(Product product)
    {
        return new ProductDto()
        {
            ImageName = product.ImageName,
            CategoryId = product.CategoryId,
            Description = product.Description,
            Count = product.Count,
            SeoData = product.SeoData,
            Id = product.Id,
            Slug = product.Slug,
            Title = product.Title,
            SubCategoryId = product.SubCategoryId,
            Price = product.Price,
            Category = product.Category == null ? null : CategoryMapper.Map(product.Category),
            SubCategory = product.SubCategory == null ? null : CategoryMapper.Map(product.SubCategory),
            Sales = (int)product.Sales,
            Ampere = product.Ampere,
            Brand = product.Brand,
        };
    }
}