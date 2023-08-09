using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace CoreLayer.Services.FileManager;

public interface IFileManager
{
    string SaveImageAndReturnImageName(IFormFile file, string savePath);
    string SaveFileAndReturnName(IFormFile file, string savePath);
    void DeleteFile(string fileName , string path);
}