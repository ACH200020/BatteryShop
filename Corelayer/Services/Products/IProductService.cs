using CoreLayer.DTOs.Category;
using CoreLayer.DTOs.Product;
using CoreLayer.Mapper;
using CoreLayer.Services.FileManager;
using CoreLayer.Utilities;
using DataLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace CoreLayer.Services.Products;

public interface IProductService
{
    OperationResult CreateProduct(CreateProductDto productDto);
    OperationResult EditProduct(EditProductDto productDto);
    ProductDto GetProductById(int productId);
    ProductDto GetProductBySlug(string slug);
    ProductFilterDto GetProductByFilterDto(ProductFilterParams filterParams);
    OperationResult DeleteProduct(int productId);
    bool IsSlugExist(string slug);
    List<ProductDto> GetAllProduct();
    public List<ProductDto> GetProductByAmpere(ProductDto dto);

}

public class ProductService: IProductService
{
    private readonly ShopContext _shopContext;
    private readonly IFileManager _FileManager;

    public ProductService(ShopContext shopContext, IFileManager fileManager)
    {
        _shopContext = shopContext;
        _FileManager = fileManager;
    }

    public OperationResult CreateProduct(CreateProductDto productDto)
    {
        if (productDto.ImageFile == null)
        {
            return OperationResult.Error();
        }

        var product = ProductMapper.MapCreateDtoToProduct(productDto);

        if(IsSlugExist(product.Slug))
            return OperationResult.Error("slug تکراری است");
        product.ImageName = _FileManager.SaveImageAndReturnImageName(productDto.ImageFile, Directories.ProductImage);
        _shopContext.Products.Add(product);
        _shopContext.SaveChanges();
        return OperationResult.Success();
    }

    public OperationResult DeleteProduct(int productId)
    {
        var product = _shopContext.Products.FirstOrDefault(f=>f.Id == productId);
        if (product == null)
            return OperationResult.NotFound();

        _shopContext.Products.Remove(product);
        _shopContext.SaveChanges();
        return OperationResult.Success();
    }

    public OperationResult EditProduct(EditProductDto productDto)
    {
        var product = _shopContext.Products.FirstOrDefault(f => f.Id == productDto.ProductId);
        if (product == null)
        {
            return OperationResult.NotFound();
        }

        var oldImage = product.ImageName;
        var newSlug = product.Slug.ToSlug();

        if (product.Slug != newSlug)
        {
            if (IsSlugExist(newSlug))
            {
                return OperationResult.Error("slug تکراری است");
            }
        }

        ProductMapper.EditProduct(productDto, product);
        if (productDto.ImageFile != null)
            product.ImageName =
                _FileManager.SaveImageAndReturnImageName(productDto.ImageFile, Directories.ProductImage);
        _shopContext.Products.Update(product);
        _shopContext.SaveChanges();

        if (productDto.ImageFile != null)
        {
            _FileManager.DeleteFile(oldImage,Directories.ProductImage);
        }

        return OperationResult.Success();
    }

    public ProductFilterDto GetProductByFilterDto(ProductFilterParams filterParams)
    {
        var result = _shopContext.Products
               .Include(d => d.Category)
               .Include(d => d.SubCategory)
               .OrderByDescending(d => d.Id)
               .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filterParams.CategorySlug))
            result = result.Where(r => r.Category.Slug == filterParams.CategorySlug
                                       || r.SubCategory.Slug == filterParams.CategorySlug);

        if (!string.IsNullOrWhiteSpace(filterParams.Title))
            result = result.Where(r => r.Title.Contains(filterParams.Title));

        var skip = (filterParams.PageId - 1) * filterParams.Take;
        var model = new ProductFilterDto()
        {
            Products = result.Skip(skip).Take(filterParams.Take)
                .Select(post => ProductMapper.MapToDto(post)).ToList(),
            FilterParams = filterParams,
        };
        model.GeneratePaging(result, filterParams.Take, filterParams.PageId);

        return model;
    }

    public ProductDto? GetProductById(int productId)
    {
        var product = _shopContext.Products
            .Include(c => c.SubCategory)
            .Include(c => c.Category)
            .FirstOrDefault(c => c.Id == productId);
        if (product == null)
            return null;

        return ProductMapper.MapToDto(product);
    }

    public ProductDto? GetProductBySlug(string slug)
    {
        var product = _shopContext.Products
            .Include(c => c.SubCategory)
            .Include(c => c.Category)
            .FirstOrDefault(c => c.Slug == slug);
        if (product == null)
            return null;

        return ProductMapper.MapToDto(product);
    }

    public bool IsSlugExist(string slug)
    {
        return _shopContext.Products.Any(c => c.Slug == slug.ToSlug());
    }

    public List<ProductDto> GetAllProduct()
    {
        return _shopContext.Products.Where(f=>f.Count!=0).Select(product => ProductMapper.MapToDto(product)).ToList();
    }

    public List<ProductDto> GetProductByAmpere(ProductDto dto)
    {
        return _shopContext.Products.Where(f => f.Ampere == dto.Ampere).Select(product => ProductMapper.MapToDto(product)).ToList();
    }
}