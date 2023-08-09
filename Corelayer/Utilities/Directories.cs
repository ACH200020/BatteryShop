namespace CoreLayer.Utilities;

public class Directories
{
    public const string ProductImage = "wwwroot/images/product";
    public const string ProductContentImage = "wwwroot/images/product/content";

    public static string GetProductImage(string imageName) 
        => $"{ProductImage.Replace("wwwroot", "")}/{imageName}";

    public static string GetProductContentImage(string imageName)
        => $"{ProductContentImage.Replace("wwwroot", "")}/{imageName}";

}