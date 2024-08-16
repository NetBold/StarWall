using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StarWall.Core.Enums;
using StarWall.Core.Generators;
using StarWall.Core.Interfaces;

namespace StarWall.Core.FileServices;

public class FileManager : IFileManager
{
    private readonly IWebHostEnvironment _environment;
    private readonly IConfiguration _configuration;
    private readonly string _baseAddress = "";

    public FileManager(IWebHostEnvironment environment, IConfiguration configuration)
    {
        _environment = environment;
        _configuration = configuration;
        _baseAddress = _configuration["BaseAddress"];
    }

    public async Task<string> UploadImage(IFormFile file, UploadImageType uploadImageType)
    {
        string imageName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
        string imagePath = "";
        switch (uploadImageType)
        {
            case UploadImageType.Wallpaper:
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles", "WallpaperImages", imageName);
                break;
            case UploadImageType.UserProfile:
                string x = _environment.WebRootPath;
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles", "ProfileImages", imageName);
                break;
        }
        var memoryStream = new MemoryStream();
        await file.OpenReadStream().CopyToAsync(memoryStream);

        await using (var fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
        {
            memoryStream.WriteTo(fs);
        }
        return imageName;
    }

    public string GetUserProfilesFolder()
    {
        string path = $"{Path.Combine(_environment.WebRootPath, "UploadedFiles", "ProfileImages")}";
        return path;
    }

    public void DeleteImage(string fileName, UploadImageType uploadImageType)
    {
        string imagePath = "";
        switch (uploadImageType)
        {
            case UploadImageType.Wallpaper:
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles", "WallpaperImages", fileName);
                break;
            case UploadImageType.UserProfile:
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles", "ProfileImages", fileName);
                break;
        }
        File.Delete(imagePath);
    }

    public string GetImagePath(string fileName, UploadImageType uploadImageType)
    {
        string path = "";
        switch (uploadImageType)
        {
            case UploadImageType.Wallpaper:
                path = Path.Combine(_baseAddress, "UploadedFiles", "WallpaperImages", fileName);
                break;
            case UploadImageType.UserProfile:
                path = Path.Combine(_baseAddress, "UploadedFiles", "ProfileImages", fileName);
                break;
        }
        return path;
    }

    //public static Stream GetFileStream(string path)
    //{
    //    return File.OpenRead(@"http://api.starwall.site/UploadedFiles/WallpaperImages/knu2bb5y.ufw.jpg");
    //}
}