using StarWall.Core.DTOs;
using StarWall.Core.Models;
using StarWall.Domain.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWall.Core.Interfaces
{
    public interface IUserManager
    {
        Task<UserManagerActionResult> LoginUser(LoginUserDTO loginUserDTO);
        Task<UserManagerActionResult> RegisterUser(RegisterUserDTO registerUserDTO);
        Task<IList<User>> GetAllUsers();
        Task SendUserActivationToken(string to, string userToekn);
    }
}
