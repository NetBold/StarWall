using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarWall.Core.DTOs;
using StarWall.Core.Enums;
using StarWall.Core.FileServices;
using StarWall.Core.Interfaces;
using StarWall.Core.Models;
using StarWall.Core.Security;
using StarWall.Core.Services;
using StarWall.Domain.UserEntities;
using StarWall.Persistence.Services;

namespace StarWall.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserManager _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public AccountController(ILogger<AccountController> logger, IUserManager userManager, IUnitOfWork unitOfWork, IFileManager fileManager, IMapper mapper, IWebHostEnvironment env)
        {
            _logger = logger;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _mapper = mapper;
            _env = env;
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userManager.GetAllUsers());
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUserDTO)
        {
            _logger.LogInformation($"POST Attempt for {nameof(Register)}");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST Attempt for {nameof(Register)}");
                return BadRequest(ModelState);
            }
            var result = await _userManager.RegisterUser(registerUserDTO);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok($"{result.User.ActiveToken}");
        }

        [HttpDelete("DeleteAllUsers")]
        public async Task<IActionResult> DeleteAllUsers()
        {
            var users = await _userManager.GetAllUsers();
            foreach (var user in users)
            {
                await _unitOfWork.Users.Delete(user.Id);
            }
            await _unitOfWork.Save();
            return Ok();
        }

        [HttpPost("UploadUserProfileFile")]
        public async Task<IActionResult> UploadUserProfileFile([FromForm] IFormFile file, [FromQuery] string activeToken)
        {
            if (file != null && activeToken != null)
            {
                string extention = Path.GetExtension(file.FileName);
                if (extention.ToLower() == ".jpg" || extention.ToLower() == ".png" || extention.ToLower() == ".jpeg")
                {
                    string imageName = await _fileManager.UploadImage(file, Core.Enums.UploadImageType.UserProfile);
                    var user = await _unitOfWork.Users.Get(x => x.ActiveToken == activeToken);
                    if (user != null)
                    {
                        user.ProfileImage = imageName;
                        user.ActiveToken = Guid.NewGuid().ToString();
                        _unitOfWork.Users.Update(user);
                        await _unitOfWork.Save();
                        return Ok();
                    }
                    return BadRequest("Token is INVALID");
                }
                return BadRequest("Invalid file type");
            }
            return BadRequest("File or Token is null");
        }

        [HttpPost("UpdateUserProfileFile")]
        public async Task<IActionResult> UpdateUserProfileFile([FromForm] IFormFile file, [FromQuery] string activeToken)
        {
            if (file != null && activeToken != null)
            {
                string extention = Path.GetExtension(file.FileName);
                if (extention.ToLower() == ".jpg" || extention.ToLower() == ".png" || extention.ToLower() == ".jpeg")
                {
                    string imageName = await _fileManager.UploadImage(file, Core.Enums.UploadImageType.UserProfile);
                    var user = await _unitOfWork.Users.Get(x => x.ActiveToken == activeToken);
                    _fileManager.DeleteImage(user.ProfileImage, Core.Enums.UploadImageType.UserProfile);
                    if (user != null)
                    {
                        user.ProfileImage = imageName;
                        user.ActiveToken = Guid.NewGuid().ToString();
                        _unitOfWork.Users.Update(user);
                        await _unitOfWork.Save();
                        return Ok();
                    }
                    return BadRequest("Token is INVALID");
                }
                return BadRequest("Invalid file type");
            }
            return BadRequest("File or Token is null");
        }

        [HttpPost("SendActivationMail")]
        public async Task<IActionResult> SendActivationMail([FromBody] LoginUserDTO loginUserDTO)
        {
            _logger.LogInformation($"POST Attempt for {nameof(SendActivationMail)}");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST Attempt for {nameof(SendActivationMail)}");
                return BadRequest();
            }
            string username = loginUserDTO.Username;
            string hashedPassword = PasswordHelper.EncodePasswordMd5(loginUserDTO.Password);
            var user = await _unitOfWork.Users.Get(x => x.Username == username && x.HashedPassword == hashedPassword);
            if (user == null)
            {
                return BadRequest("Invalid Info");
            }
            if (user.IsActive)
            {
                return BadRequest("Your account is active!");
            }
            await _userManager.SendUserActivationToken(user.Email, user.ActiveToken);
            return Ok("The activation TOKEN was sent to your email!");
        }

        [HttpPost("ActivateAccount")]
        public async Task<IActionResult> ActivateAccount([FromBody] AccountActivationDTO accountActivationDTO)
        {
            _logger.LogInformation($"POST Attempt for {nameof(ActivateAccount)}");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST Attempt for {nameof(ActivateAccount)}");
                return BadRequest("Model state is Invalid");
            }
            string username = accountActivationDTO.Username;
            string token = accountActivationDTO.UserToeken;
            var user = await _unitOfWork.Users.Get(x => x.Username == username && x.ActiveToken == token);
            if (user == null)
            {
                return BadRequest("Invalid Data");
            }
            user.IsActive = true;
            user.ActiveToken = Guid.NewGuid().ToString();
            _unitOfWork.Users.Update(user);
            await _unitOfWork.Save();
            return Ok("Account is Active Now!");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
        {
            _logger.LogInformation($"POST Attempt for {nameof(Login)}");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST Attempt for {nameof(Login)}");
                return BadRequest(ModelState);
            }
            var result = await _userManager.LoginUser(loginUserDTO);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Token);
        }

        [Authorize]
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser([FromHeader] string Authorization)
        {
            _logger.LogInformation($"GET Attempt for {nameof(GetUser)}");
            string token = Authorization.Replace("bearer ", string.Empty);
            DecodedJWTModel decodedJWTModel = JWTHelper.GetDecodedJWTModelByToken(token);
            var user = await _unitOfWork.Users.Get(x => x.Username == decodedJWTModel.Username, includes: new List<string> { "Role", "UploadedWallpapers", "DownloadedWallpapers", "Likes" });
            if (user != null)
            {
                var result = _mapper.Map<UserDTO>(user);
                result.ProfileImage = _fileManager.GetImagePath(result.ProfileImage, UploadImageType.UserProfile);
                result.LastLikedWallpapers = new();
                foreach (var like in user.Likes.TakeLast(4))
                {
                    var wallpaper = await _unitOfWork.Wallpapers.Get(x => x.Id == like.WallpaperId, includes: new List<string> { "WallpaperImages" });
                    result.LastLikedWallpapers.Add(wallpaper);
                }
                return Ok(result);
            }
            return BadRequest("Invalid Token");
        }

        [Authorize]
        [HttpGet("GetUserForUpdate")]
        public async Task<IActionResult> GetUserForUpdate([FromHeader] string Authorization)
        {
            _logger.LogInformation($"GET Attempt for {nameof(GetUserForUpdate)}");
            string token = Authorization.Replace("bearer ", string.Empty);
            DecodedJWTModel decodedJWTModel = JWTHelper.GetDecodedJWTModelByToken(token);
            var user = await _unitOfWork.Users.Get(x => x.Username == decodedJWTModel.Username);
            if (user != null)
            {
                var result = _mapper.Map<UpdateUserDTO>(user);
                result.ProfileImage = _fileManager.GetImagePath(result.ProfileImage, UploadImageType.UserProfile); ;
                return Ok(result);
            }
            return BadRequest("Invalid Token");
        }

        [Authorize]
        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromHeader] string Authorization, [FromBody] UpdateUserDTO updateUserDTO)
        {
            _logger.LogInformation($"POST Attempt for {nameof(UpdateUser)}");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST Attempt for {nameof(UpdateUser)}");
                return BadRequest(ModelState);
            }
            _logger.LogInformation($"GET Attempt for {nameof(GetUserForUpdate)}");
            string token = Authorization.Replace("bearer ", string.Empty);
            DecodedJWTModel decodedJWTModel = JWTHelper.GetDecodedJWTModelByToken(token);
            var user = await _unitOfWork.Users.Get(x => x.Username == decodedJWTModel.Username);
            if (user != null)
            {
                user.FirstName = updateUserDTO.FirstName;
                user.LastName = updateUserDTO.LastName;
                user.Username = updateUserDTO.Username;
                _unitOfWork.Users.Update(user);
                await _unitOfWork.Save();
                return Ok(user.ActiveToken);
            }
            return BadRequest("Invalid Token");
        }

        [Authorize]
        [HttpPost("ResetPasswordInUserPanel")]
        public async Task<IActionResult> ResetPasswordInUserPanel([FromHeader] string Authorization, [FromBody] ResetPasswordDTOInUserPanel resetPasswordDTO)
        {
            _logger.LogInformation($"POST Attempt for {nameof(ResetPasswordInUserPanel)}");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST Attempt for {nameof(ResetPasswordInUserPanel)}");
                return BadRequest(ModelState);
            }
            string token = Authorization.Replace("bearer ", string.Empty);
            DecodedJWTModel decodedJWTModel = JWTHelper.GetDecodedJWTModelByToken(token);
            var user = await _unitOfWork.Users.Get(x => x.Username == decodedJWTModel.Username);
            if (user != null)
            {
                string currentHashedPassword = PasswordHelper.EncodePasswordMd5(resetPasswordDTO.CurrentPassword);
                if (currentHashedPassword == user.HashedPassword)
                {
                    user.HashedPassword = PasswordHelper.EncodePasswordMd5(resetPasswordDTO.NewPassword);
                    _unitOfWork.Users.Update(user);
                    await _unitOfWork.Save();
                    return Ok("Password Changed Successfully");
                }
                else
                {
                    return BadRequest("Current password is not correct!");
                }
            }
            return BadRequest("Invalid Token");
        }

        [HttpPost("SendUserPasswordToken")]
        public async Task<IActionResult> SendUserPasswordToken([FromBody] EmailDTO emailDTO)
        {
            _logger.LogInformation($"POST Attempt for {nameof(SendUserPasswordToken)}");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST Attempt for {nameof(SendUserPasswordToken)}");
                return BadRequest(ModelState);
            }
            var user = await _unitOfWork.Users.Get(x => x.Email == emailDTO.Email);
            if (user != null)
            {
                string to = user.Email;
                string subject = $"Password Reset for {user.Username}";
                string userActiveToken = user.ActiveToken;
                string body = $"Your Token is : {userActiveToken}";
                await EmailService.Send(to, subject, body);
                return Ok("Email Sent!");
            }
            return BadRequest("No user found with this Email Address!");
        }

        [HttpPost("ResetPasswordFromForgotPassword")]
        public async Task<IActionResult> ResetPasswordFromForgotPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            _logger.LogInformation($"POST Attempt for {nameof(ResetPasswordFromForgotPassword)}");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST Attempt for {nameof(ResetPasswordFromForgotPassword)}");
                return BadRequest(ModelState);
            }
            var user = await _unitOfWork.Users.Get(x => x.Email == resetPasswordDTO.Email && x.ActiveToken == resetPasswordDTO.Token);
            if (user != null)
            {
                user.HashedPassword = PasswordHelper.EncodePasswordMd5(resetPasswordDTO.NewPassword);
                user.ActiveToken = Guid.NewGuid().ToString();
                _unitOfWork.Users.Update(user);
                await _unitOfWork.Save();
                return Ok("Password Changed Successfully");
            }
            return BadRequest("Email Address or Token is invalid");
        }

        [Authorize]
        [HttpGet("TestAuthGet")]
        public async Task<IActionResult> TestAuthGet()
        {
            return Ok("Auth Success!");
        }
    }
}
