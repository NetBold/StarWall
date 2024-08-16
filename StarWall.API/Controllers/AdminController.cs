using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarWall.Core.DTOs;
using StarWall.Core.Enums;
using StarWall.Core.FileServices;
using StarWall.Core.Interfaces;
using StarWall.Core.Models;
using StarWall.Domain.WallpaperEntities;
using System.Linq;

namespace StarWall.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AdminController> _logger;
        private readonly IMapper _mapper;
        private readonly IFileManager _fileManager;

        public AdminController(IUnitOfWork unitOfWork, ILogger<AdminController> logger, IMapper mapper, IFileManager fileManager)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _fileManager = fileManager;
        }

        #region WebInfoActions
        [Authorize(Roles = "Admin")]
        [HttpGet("GetWebInfoForEdit")]
        public async Task<IActionResult> GetWebInfoForEdit()
        {
            _logger.LogInformation($"GET Attempt for {nameof(GetWebInfoForEdit)}");
            var allWebSiteInfos = await _unitOfWork.WebSiteInfos.GetAll();
            var webSiteInfo = allWebSiteInfos.FirstOrDefault();
            UpdateWebSiteInfoDTO webSiteInfoDTO = _mapper.Map<UpdateWebSiteInfoDTO>(webSiteInfo);
            return Ok(webSiteInfoDTO);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateWebInfo")]
        public async Task<IActionResult> UpdateWebInfo([FromQuery] long id, [FromBody] UpdateWebSiteInfoDTO updateWebSiteInfoDTO)
        {
            var user = User;
            _logger.LogInformation($"PUT Attempt for {nameof(UpdateWebInfo)}");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid PUT Attempt for {nameof(UpdateWebInfo)}");
                return BadRequest(ModelState);
            }
            var allWebInfos = await _unitOfWork.WebSiteInfos.GetAll();
            var webInfo = allWebInfos.FirstOrDefault();
            if (webInfo == null)
            {
                _logger.LogError($"Invalid PUT Attempt for {nameof(UpdateWebInfo)}");
                return BadRequest($"No webInfo with the Id : {id}");
            }
            _mapper.Map(updateWebSiteInfoDTO, webInfo);
            _unitOfWork.WebSiteInfos.Update(webInfo);
            await _unitOfWork.Save();
            return Ok("Updated Successfully");
        }
        #endregion

        #region ContactMessagesActions
        [Authorize(Roles = "Admin")]
        [HttpGet("GetUnReadMessages")]
        public async Task<IActionResult> GetUnReadMessages()
        {
            _logger.LogInformation($"Get Attempt for {nameof(GetUnReadMessages)}");
            var unreadMessages = await _unitOfWork.Contacts.GetAll(x => x.IsSeenByAdmin == false, orderBy: o => o.OrderByDescending(x => x.CreationDate));
            var result = _mapper.Map<List<ContactDTO>>(unreadMessages);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetReadMessages")]
        public async Task<IActionResult> GetReadMessages()
        {
            _logger.LogInformation($"Get Attempt for {nameof(GetUnReadMessages)}");
            var unreadMessages = await _unitOfWork.Contacts.GetAll(x => x.IsSeenByAdmin == true, orderBy: o => o.OrderByDescending(x => x.CreationDate));
            var result = _mapper.Map<List<ContactDTO>>(unreadMessages);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteUnReadMessage/{id:long}")]
        public async Task<IActionResult> DeleteUnReadMessage(long id = 0)
        {
            _logger.LogInformation($"Delete Attempt for {nameof(DeleteUnReadMessage)}");
            if (id != 0)
            {
                var contact = await _unitOfWork.Contacts.Get(x => x.Id == id);
                if (contact != null)
                {
                    await _unitOfWork.Contacts.Delete(id);
                    await _unitOfWork.Save();
                    return Ok("Deleted Successfully");
                }
                else
                {
                    return BadRequest("Id is invalid");
                }
            }
            _logger.LogInformation($"Invalid Delete Attempt for {nameof(DeleteUnReadMessage)}");
            return BadRequest("id is empty");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetContactMessage/{id:long}")]
        public async Task<IActionResult> GetContactMessage(long id = 0)
        {
            _logger.LogInformation($"GET Attempt for {nameof(GetContactMessage)}");
            if (id != 0)
            {
                var contact = await _unitOfWork.Contacts.Get(x => x.Id == id);
                if (contact != null)
                {
                    return Ok(contact);
                }
                else
                {
                    return BadRequest("Id is invalid");
                }
            }
            _logger.LogInformation($"Invalid GET Attempt for {nameof(DeleteUnReadMessage)}");
            return BadRequest("id is empty");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("MakeMessageSeen/{id:long}")]
        public async Task<IActionResult> MakeMessageSeen(long id = 0)
        {
            _logger.LogInformation($"PUT Attempt for {nameof(MakeMessageSeen)}");
            if (id != 0)
            {
                var contact = await _unitOfWork.Contacts.Get(x => x.Id == id);
                if (contact != null)
                {
                    contact.IsSeenByAdmin = true;
                    _unitOfWork.Contacts.Update(contact);
                    await _unitOfWork.Save();
                    return Ok("Seen!");
                }
                else
                {
                    return BadRequest("Id is invalid");
                }
            }
            _logger.LogInformation($"Invalid PUT Attempt for {nameof(MakeMessageSeen)}");
            return BadRequest("id is empty");
        }
        #endregion

        #region WallpaperActions

        [Authorize(Roles = "Admin,Uploader")]
        [HttpGet("GetWallpaperForEdit/{id:long}")]
        public async Task<IActionResult> GetWallpaperForEdit(long id)
        {
            _logger.LogInformation($"GET Attempt for {nameof(GetWallpaperForEdit)}");
            if (id > 0)
            {
                var wallpaper = await _unitOfWork.Wallpapers.Get(x => x.Id == id, includes: new List<string> { "WallpaperGenreCategory", "WallpaperImages" });
                if (wallpaper != null)
                {
                    EditWallpaperDTO editWallpaperDTO = _mapper.Map<EditWallpaperDTO>(wallpaper);
                    editWallpaperDTO.WallpaperGenreCategoryTitle = wallpaper.WallpaperGenreCategory.Title;
                    WallpaperImage desktopWallpaperImage = wallpaper.WallpaperImages.SingleOrDefault(x => x.WallpaperDeviceCategoryId == 2);
                    editWallpaperDTO.DesktopImagePath = _fileManager.GetImagePath(desktopWallpaperImage.FileName, UploadImageType.Wallpaper);
                    WallpaperImage mobileWallpaperImage = wallpaper.WallpaperImages.SingleOrDefault(x => x.WallpaperDeviceCategoryId == 1);
                    if (mobileWallpaperImage != null)
                        editWallpaperDTO.MobileImagePath = _fileManager.GetImagePath(mobileWallpaperImage.FileName, UploadImageType.Wallpaper);
                    return Ok(editWallpaperDTO);
                }
                return BadRequest("Invalid Id");
            }
            _logger.LogError($"Invalid GET Attempt for {nameof(GetWallpaperForEdit)}");
            return BadRequest("Id is 0");
        }

        [Authorize(Roles = "Admin,Uploader")]
        [HttpGet("GetWallpapersForAdminPanel")]
        public async Task<IActionResult> GetWallpapersForAdminPanel()
        {
            _logger.LogInformation($"GET Attempt for {nameof(GetWallpapersForAdminPanel)}");
            var wallpapers = await _unitOfWork.Wallpapers.GetAll(includes: new List<string> { "WallpaperGenreCategory", "WallpaperImages", "Likes", "Uploader" }, orderBy: x => x.OrderByDescending(x => x.PublishDate));
            var result = _mapper.Map<List<AdminPanelWallpaperDTO>>(wallpapers);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Uploader")]
        [HttpPost("CreateWallpaper")]
        public async Task<IActionResult> CreateWallpaper([FromBody] CreateWallpaperDTO createWallpaperDto)
        {
            _logger.LogInformation($"POST Attempt for {nameof(CreateWallpaper)}");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST Attempt for {nameof(CreateWallpaper)}");
                return BadRequest(ModelState);
            }
            var wallpaper = _mapper.Map<Wallpaper>(createWallpaperDto);
            string genreTitle = createWallpaperDto.WallpaperGenreCategoryTitle;
            var genre = await _unitOfWork.WallpaperGenreCategories.Get(x => x.Title == genreTitle);
            wallpaper.WallpaperGenreCategory = null;
            wallpaper.WallpaperGenreCategoryId = genre.Id;
            wallpaper.PublishDate = DateTime.Now;
            await _unitOfWork.Wallpapers.Insert(wallpaper);
            await _unitOfWork.Save();
            return Ok(wallpaper.Id);
        }

        [Authorize(Roles = "Admin,Uploader")]
        [HttpPost("UploadWallpaperFile/{id:long}/{deviceId:int}")]
        public async Task<IActionResult> UploadWallpaperFile([FromForm] IFormFile file, long id, int deviceId, bool isUpdate)
        {
            if (file != null)
            {
                string extention = Path.GetExtension(file.FileName);
                FileInfo fileInfo = new FileInfo(file.FileName);
                bool isValidType = MimeHelper.IsImageFileTypeValid(fileInfo);
                if (isValidType)
                {
                    Wallpaper wallpaper = await _unitOfWork.Wallpapers.Get(x => x.Id == id, includes: new List<string> { "WallpaperImages" });
                    if (wallpaper != null)
                    {
                        string imageName = await _fileManager.UploadImage(file, UploadImageType.Wallpaper);
                        WallpaperImage wallpaperImage = new()
                        {
                            FileName = imageName,
                            WallpaperDeviceCategoryId = deviceId,
                            WallpaperId = id
                        };

                        await _unitOfWork.WallpaperImages.Insert(wallpaperImage);
                        await _unitOfWork.Save();
                        return Ok();
                    }
                    return BadRequest("Wallpaper Id is Invalid");
                }
                return BadRequest("Invalid file type");
            }
            return BadRequest("File or Token is null");
        }

        [Authorize(Roles = "Admin,Uploader")]
        [HttpPut("UpdateWallpaperById/{id:long}")]
        public async Task<IActionResult> UpdateWallpaperById(long id, [FromBody] EditWallpaperDTO editWallpaperDTO)
        {
            _logger.LogInformation($"PUT Attempt for {nameof(UpdateWallpaperById)}");
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid PUT Attempt for {nameof(UpdateWallpaperById)}");
                return BadRequest(ModelState);
            }

            var wallpaper = await _unitOfWork.Wallpapers.Get(x => x.Id == id, includes: new List<string> { "WallpaperImages" });
            if (wallpaper == null)
            {
                _logger.LogError($"Invalid PUT Attempt for {nameof(UpdateWallpaperById)}");
                return BadRequest("Submited data is not valid");
            }
            if (editWallpaperDTO.IsDesktopImageUpdated)
            {
                var currentDesktopWallpaperImage = wallpaper.WallpaperImages.SingleOrDefault(x => x.WallpaperDeviceCategoryId == 2);
                if (currentDesktopWallpaperImage != null)
                {
                    _fileManager.DeleteImage(currentDesktopWallpaperImage.FileName, UploadImageType.Wallpaper);
                    await _unitOfWork.WallpaperImages.Delete(currentDesktopWallpaperImage.Id);
                }
            }
            if (editWallpaperDTO.IsMobileImageUpdated)
            {
                var currentMobileWallpaperImage = wallpaper.WallpaperImages.SingleOrDefault(x => x.WallpaperDeviceCategoryId == 1);
                if (currentMobileWallpaperImage != null)
                {
                    _fileManager.DeleteImage(currentMobileWallpaperImage.FileName, UploadImageType.Wallpaper);
                    await _unitOfWork.WallpaperImages.Delete(currentMobileWallpaperImage.Id);
                }
            }
            await _unitOfWork.Save();
            var genre = await _unitOfWork.WallpaperGenreCategories.Get(x => x.Title == editWallpaperDTO.WallpaperGenreCategoryTitle);
            _mapper.Map(editWallpaperDTO, wallpaper);
            wallpaper.WallpaperGenreCategoryId = genre.Id;
            wallpaper.WallpaperGenreCategory = null;
            _unitOfWork.Wallpapers.Update(wallpaper);
            await _unitOfWork.Save();
            return Ok();
        }

        [Authorize(Roles = "Admin,Uploader")]
        [HttpDelete("DeleteWallpaperById")]
        public async Task<IActionResult> DeleteWallpaperById([FromQuery] long id)
        {
            _logger.LogInformation($"Delete Attempt for {nameof(DeleteWallpaperById)}");
            var wallpaper = await _unitOfWork.Wallpapers.Get(x => x.Id == id, includes: new List<string> { "WallpaperImages" });
            if (wallpaper == null)
            {
                _logger.LogError($"Invalid Delete Attempt for {nameof(DeleteWallpaperById)}");
                return BadRequest("Id is invalid");
            }
            await _unitOfWork.Wallpapers.Delete(wallpaper.Id);
            await _unitOfWork.Save();
            foreach (var item in wallpaper.WallpaperImages)
            {
                _fileManager.DeleteImage(item.FileName, UploadImageType.Wallpaper);
            }
            return Ok("Deleted Successfully");
        }

        [HttpGet("GetGenreForUpdate/{id:long}")]
        public async Task<IActionResult> GetGenreForUpdate(long id)
        {
            _logger.LogInformation($"GET Attempt for {nameof(GetGenreForUpdate)}");
            var genre = await _unitOfWork.WallpaperGenreCategories.Get(x => x.Id == id);
            if (genre == null)
            {
                _logger.LogError($"Invalid GET Attempt for {nameof(GetGenreForUpdate)}");
                return BadRequest("Invalid Id!");
            }
            UpdateGenreDTO genreDTO = _mapper.Map<UpdateGenreDTO>(genre);
            return Ok(genreDTO);
        }

        [HttpGet("GetWallpaperGenreCategories")]
        public async Task<IActionResult> GetWallpaperGenreCategories()
        {
            _logger.LogInformation($"GET Attempt for {nameof(GetWallpaperGenreCategories)}");
            var wallpaperGenreCategories = await _unitOfWork.WallpaperGenreCategories.GetAll();
            return Ok(wallpaperGenreCategories);
        }

        [Authorize(Roles = "Admin,Uploader")]
        [HttpDelete("DeleteWallpaperGenreById/{id:int}")]
        public async Task<IActionResult> DeleteWallpaperGenreById(int id)
        {
            _logger.LogInformation($"Delete Attempt for {nameof(DeleteWallpaperGenreById)}");
            var genre = await _unitOfWork.WallpaperGenreCategories.Get(x => x.Id == id);
            if (genre == null)
            {
                _logger.LogError($"Invalid Delete Attempt for {nameof(DeleteWallpaperGenreById)}");
                return BadRequest("Id is invalid");
            }
            await _unitOfWork.WallpaperGenreCategories.Delete(id);
            await _unitOfWork.Save();
            return Ok("Deleted Successfully");
        }

        [Authorize(Roles = "Admin,Uploader")]
        [HttpPost("CreateGenre")]
        public async Task<IActionResult> CreateGenre([FromBody] CreateGenreDTO createGenreDTO)
        {
            _logger.LogInformation($"POST Attempt for {nameof(CreateGenre)}");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST Attempt for {nameof(CreateGenre)}");
                return BadRequest("Hnvalid Data");
            }
            var existingGenre = await _unitOfWork.WallpaperGenreCategories.Get(x => x.Title == createGenreDTO.Title);
            if (existingGenre != null)
            {
                return BadRequest("Genre with this title exists!");
            }
            WallpaperGenreCategory genre = _mapper.Map<WallpaperGenreCategory>(createGenreDTO);
            await _unitOfWork.WallpaperGenreCategories.Insert(genre);
            await _unitOfWork.Save();
            return Ok();
        }

        [Authorize(Roles = "Admin,Uploader")]
        [HttpPut("UpdateGenre/{id:long}")]
        public async Task<IActionResult> UpdateGenre(long id, [FromBody] UpdateGenreDTO updateGenreDTO)
        {
            _logger.LogInformation($"PUT Attempt for {nameof(UpdateGenre)}");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid PUT Attempt for {nameof(UpdateGenre)}");
                return BadRequest("Hnvalid Data");
            }
            var genre = await _unitOfWork.WallpaperGenreCategories.Get(x => x.Id == id);
            if (genre == null)
            {
                return BadRequest("Invalid Id!");
            }
            genre = _mapper.Map<WallpaperGenreCategory>(updateGenreDTO);
            _unitOfWork.WallpaperGenreCategories.Update(genre);
            await _unitOfWork.Save();
            return Ok();
        }

        [HttpGet("GetWallpaperDeviceCategories")]
        public async Task<IActionResult> GetWallpaperDeviceCategories()
        {
            _logger.LogInformation($"GET Attempt for {nameof(GetWallpaperDeviceCategories)}");
            var wallpaperDeviceCategories = await _unitOfWork.WallpaperDeviceCategories.GetAll();
            return Ok(wallpaperDeviceCategories);
        }
        #endregion
    }
}
