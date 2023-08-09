using CoreLayer.DTOs.Product;
using CoreLayer.DTOs.Product.ProductImage;
using CoreLayer.Mapper;
using CoreLayer.Services.FileManager;
using CoreLayer.Utilities;
using DataLayer.Context;
using DataLayer.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace CoreLayer.Services.Products.ProductImages;

public interface IProductImageService
{
    OperationResult AddImage(AddImageDto command);
    OperationResult DeleteImage(long id);
    List<ImageDto> GetImageById(long id);
    ImageFilterDto GetAllImagesByFilterDto(ImageFilterParams filterParams);
}

public class ProductImageService : IProductImageService
{
    private readonly ShopContext _shopContext;
    private readonly IFileManager _fileManager;
    private readonly IProductService _productService;

    public ProductImageService(ShopContext shopContext, IFileManager fileManager, IProductService productService)
    {
        _shopContext = shopContext;
        _fileManager = fileManager;
        _productService = productService;
    }
    public OperationResult AddImage(AddImageDto command)
    {

        if (command.ImageFile == null)
        {
            return OperationResult.Error();
        }
        var result = new ProductImage()
        {
            ProductId = command.ProductId,

        };
        result.ImageName = _fileManager.SaveImageAndReturnImageName(command.ImageFile, Directories.ProductContentImage);

        _shopContext.ProductImages.Add(result);
        _shopContext.SaveChanges();
        return OperationResult.Success();

    }

    public OperationResult DeleteImage(long id)
    {

        ImageDto command = new ImageDto();
        var result = _shopContext.ProductImages.FirstOrDefault(f => f.Id == id);
        if (result == null)
        {
            return OperationResult.NotFound();
        }

        _shopContext.ProductImages.Remove(result);
        _shopContext.SaveChanges();

        var oldImage = result.ImageName;
        _fileManager.DeleteFile(oldImage, Directories.ProductContentImage);
        return OperationResult.Success();
    }

    public List<ImageDto> GetImageById(long id)
    {
        return _shopContext.ProductImages.Select(imageProduct => new ImageDto()
        {
            Id = imageProduct.Id,
            ImageName = imageProduct.ImageName,
            Product = imageProduct.Product,
            ProductId = imageProduct.ProductId,
        }).Where(f=>f.ProductId == id).ToList();
    }

    public ImageFilterDto GetAllImagesByFilterDto(ImageFilterParams filterParams)
    {
        var result = _shopContext.ProductImages.Include(f => f.Product)
            .OrderByDescending(f => f.Id)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filterParams.ProductTitle))
            result = result.Where(r => r.Product.Title == filterParams.ProductTitle);

        var model = new ImageFilterDto()
        {
            ImageDtos = result.Select(post => new ImageDto()
            {
                Id = post.Id,
                ImageName = post.ImageName,
                ProductId = post.ProductId,
                Product = post.Product,
            }).ToList(),
            FilterParams = filterParams
        };
        

        return model;


    }


}