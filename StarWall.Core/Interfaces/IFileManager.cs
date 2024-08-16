using Microsoft.AspNetCore.Http;
using StarWall.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWall.Core.Interfaces
{
    public interface IFileManager
    {
        Task<string> UploadImage(IFormFile file, UploadImageType uploadImageType);
        void DeleteImage(string fileName, UploadImageType uploadImageType);
        string GetImagePath(string fileName, UploadImageType uploadImageType);
        string GetUserProfilesFolder();

    }
}
