using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using StarWall.Core.DTOs;
using StarWall.Core.Enums;
using StarWall.Core.FileServices;
using StarWall.Core.Interfaces;
using StarWall.Core.Models;
using StarWall.Core.Security;
using StarWall.Domain.WallpaperEntities;
using X.PagedList;

namespace StarWall.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WallpaperController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IFileManager _fileManager;
        private readonly IConfiguration _configuration;

        public WallpaperController(IUnitOfWork unitOfWork, ILogger<WallpaperController> logger, IMapper mapper, IFileManager fileManager, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _fileManager = fileManager;
            _configuration = configuration;
        }

        //[HttpGet(Name = "GetWallpapers")]
        //public async Task<IActionResult> GetWallpapers([FromQuery] RequestParams requestParams,
        //    [FromQuery] RequestCategoryParams requestCategoryParams)
        //{
        //    _logger.LogInformation($"GET Attempt for {nameof(GetWallpapers)}");
        //    int deviceCategoryId = requestCategoryParams.DeviceCategoryId;
        //    int genreCatgoryId = requestCategoryParams.GenreCategoryId;
        //    var wallpapers = await _unitOfWork.Wallpapers.GetAll();
        //    if (deviceCategoryId > 0 || genreCatgoryId > 0)
        //    {
        //        if (genreCatgoryId > 0)
        //        {
        //            wallpapers = wallpapers.Where(x => x.WallpaperGenreCategoryId == genreCatgoryId).ToList();
        //        }

        //        if (deviceCategoryId > 0)
        //        {
        //            wallpapers = wallpapers.Where(x => x.WallpaperDeviceCategoryId == deviceCategoryId).ToList();
        //        }
        //    }

        //    var resultWallpapers = wallpapers.ToPagedList(requestParams.PageNumber, requestParams.PageSize);
        //    var result = _mapper.Map<IList<Wallpaper>>(resultWallpapers);
        //    return Ok(result);
        //}        

        //[HttpGet(template: "{id:long}", Name = "GetWallpaperById")]
        //public async Task<IActionResult> GetWallpaperById(long id)
        //{
        //    _logger.LogInformation($"GET Attempt for {nameof(GetWallpaperById)}");
        //    var wallpaper =
        //        await _unitOfWork.Wallpapers.Get(expression: x => x.Id == id,
        //            includes: new List<string>
        //                { "Uploader", "WallpaperDeviceCategory", "WallpaperGenreCategory", "Likes" });
        //    wallpaper.ViewsCount++;
        //    var result = _mapper.Map<WallpaperDTO>(wallpaper);
        //    result.LikesCount = wallpaper.Likes.Count;
        //    _unitOfWork.Wallpapers.Update(wallpaper);
        //    await _unitOfWork.Save();
        //    return Ok(result);
        //}

        [HttpGet("GetAllWallpapers")]
        public async Task<IActionResult> GetAllWallpapers()
        {
            _logger.LogInformation($"GET Attempt for {nameof(GetAllWallpapers)}");
            var wallpapers = await _unitOfWork.Wallpapers.GetAll(includes: new List<string> { "WallpaperGenreCategory", "WallpaperImages", "Likes", "Uploader" }, orderBy: x => x.OrderByDescending(x => x.PublishDate));
            var result = _mapper.Map<List<ViewWallpaperDTO>>(wallpapers);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("DownloadWallpaper/{id:long}")]
        public async Task<IActionResult> DownloadWallpaper(long id, [FromQuery] string device)
        {
            _logger.LogInformation($"GET Attempt for {nameof(DownloadWallpaper)}");
            var wallpaper = await _unitOfWork.Wallpapers.Get(x => x.Id == id, includes: new List<string> { "WallpaperImages" });
            var deviceCategory = await _unitOfWork.WallpaperDeviceCategories.Get(x => x.Title == device);
            if (deviceCategory != null && wallpaper != null)
            {
                wallpaper.DownloadsCount++;
                _unitOfWork.Wallpapers.Update(wallpaper);
                await _unitOfWork.Save();
                var wallpaperImage = wallpaper.WallpaperImages.SingleOrDefault(x => x.WallpaperDeviceCategoryId == deviceCategory.Id);
                if(wallpaperImage == null) 
                {
                    return BadRequest($"No {device} Image");
                }
                return Ok(Path.Combine(_configuration["BaseAddress"], "UploadedFiles", "WallpaperImages", wallpaperImage.FileName ));
            }
            return BadRequest("Invalid Id or Device");
        }

        [HttpGet("GetWallpaperForView/{id:long}/{seen:bool}")]
        public async Task<IActionResult> GetWallpaperForView(long id, bool seen)
        {
            _logger.LogInformation($"GET Attempt for {nameof(GetWallpaperForView)}");
            if (id > 0)
            {
                var wallpaper = await _unitOfWork.Wallpapers.Get(x => x.Id == id, includes: new List<string> { "Uploader", "WallpaperGenreCategory", "Likes", "WallpaperImages" });
                var result = _mapper.Map<ViewWallpaperDTO>(wallpaper);
                for (int i = 0; i < result.WallpaperImages.Count; i++)
                {
                    result.WallpaperImages[i].FileName = $"{_configuration["BaseAddress"]}UploadedFiles/WallpaperImages/{result.WallpaperImages[i].FileName}";
                }
                if (seen)
                {
                    wallpaper.ViewsCount++;
                    _unitOfWork.Wallpapers.Update(wallpaper);
                    await _unitOfWork.Save();
                }
                return Ok(result);
            }
            _logger.LogError($"Invalid GET Attempt for {nameof(GetWallpaperForView)}");
            return BadRequest("Id is Invalid");
        }

        //[HttpDelete("DeleteAll")]
        //public async Task<IActionResult> DeleteAllWallpapers()
        //{
        //    var wallpapers = await _unitOfWork.Wallpapers.GetAll();
        //    foreach (var wallpaper in wallpapers)
        //    {
        //        await _unitOfWork.Wallpapers.Delete(wallpaper.Id);
        //    }
        //    await _unitOfWork.Save();
        //    return Ok();
        //}

        [Authorize]
        [HttpPost("LikeWallpaper/{id:long}")]
        public async Task<IActionResult> LikeWallpaper(long id, [FromHeader] string Authorization)
        {
            string token = Authorization.Replace("bearer ", string.Empty);
            DecodedJWTModel decodedJWTModel = JWTHelper.GetDecodedJWTModelByToken(token);
            var user = await _unitOfWork.Users.Get(x => x.Username == decodedJWTModel.Username);
            long userId = user.Id;
            _logger.LogInformation($"POST Attempt for {nameof(LikeWallpaper)}");
            var wallpaper = await _unitOfWork.Wallpapers.Get(x => x.Id == id);
            if (wallpaper == null)
            {
                _logger.LogError($"Invalid POST Attempt for {nameof(GetWallpaperForView)}");
                return BadRequest("Invalid Id");
            }
            var like = await _unitOfWork.Likes.Get(x => x.LikerId == userId && x.WallpaperId == id);
            if (like == null)
            {
                Like newLike = new()
                {
                    WallpaperId = id,
                    LikerId = userId
                };
                await _unitOfWork.Likes.Insert(newLike);
            }
            else
            {
                await _unitOfWork.Likes.Delete(like.Id);
            }

            await _unitOfWork.Save();
            return Ok();
        }
    }
}