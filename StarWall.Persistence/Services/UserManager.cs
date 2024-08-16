using AutoMapper;
using StarWall.Core.DTOs;
using StarWall.Core.Interfaces;
using StarWall.Core.Models;
using StarWall.Core.Security;
using StarWall.Core.Services;
using StarWall.Domain.UserEntities;

namespace StarWall.Persistence.Services
{
    public class UserManager : IUserManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;
        private readonly IFileManager _fileManager;

        UserManagerActionResult _result;

        public UserManager(IUnitOfWork unitOfWork, IMapper mapper, IAuthManager authManager, IFileManager fileManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authManager = authManager;
            _fileManager = fileManager;
        }

        public async Task SendUserActivationToken(string to, string userToekn)
        {
            string subject = "StarWall User Account Activation";
            string body = $"Your Activation Token is : {userToekn}";
            await EmailService.Send(to, subject, body);
        }

        public async Task<IList<User>> GetAllUsers()
        {
            var users = await _unitOfWork.Users.GetAll();
            return users;
        }

        public async Task<UserManagerActionResult> LoginUser(LoginUserDTO loginUserDTO)
        {
            var _result = new UserManagerActionResult();
            var password = PasswordHelper.EncodePasswordMd5(loginUserDTO.Password);
            var user = await _unitOfWork.Users.Get(x => x.Username == loginUserDTO.Username && x.HashedPassword == password, includes: new List<string> { "Role" });
            if (user != null)
            {
                if (user.IsActive)
                {
                    _result.User = user;
                    _result.IsSuccess = true;
                    _result.Token = _authManager.GenerateJWTToken(user);
                }
                else
                {
                    _result.ErrorMessage = "The user is not active! To activate your account go to login page and click on Activate My Account";
                    _result.IsSuccess = false;
                }
                return _result;
            }
            _result.IsSuccess = false;
            _result.ErrorMessage = "Wrong Information!";
            return _result;
        }

        public async Task<UserManagerActionResult> RegisterUser(RegisterUserDTO registerUserDTO)
        {
            _result = new UserManagerActionResult();
            var existingUser = await _unitOfWork.Users.Get(x => x.Username == registerUserDTO.Username || x.Email == registerUserDTO.Email);
            if (existingUser != null)
            {
                _result.ErrorMessage = "A User with this Username or Email exists!";
                _result.IsSuccess = false;
                return _result;
            }
            User user = _mapper.Map<User>(registerUserDTO);
            user.ProfileImage = "DefaultUserProfile.png";
            user.ActiveToken = Guid.NewGuid().ToString();
            user.RoleId = 2;
            user.RegisterDate = DateTime.Now;
            user.HashedPassword = PasswordHelper.EncodePasswordMd5(registerUserDTO.Password);
            user.IsActive = false;
            await _unitOfWork.Users.Insert(user);
            await _unitOfWork.Save();
            _result.IsSuccess = true;
            _result.User = user;
            return _result;
        }
    }
}
