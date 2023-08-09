using CoreLayer.Utilities;

namespace CoreLayer.DTOs.Product;

public class ProductFilterDto : BasePagination
{
    public List<ProductDto> Products { get; set; }
    public ProductFilterParams FilterParams { get; set; }
}

public class ProductFilterParams
{
    public string Title { get; set; }
    public string CategorySlug { get; set; }
    public int PageId { get; set; }
    public int Take { get; set; }
}