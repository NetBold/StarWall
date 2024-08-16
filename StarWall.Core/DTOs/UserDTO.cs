using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using StarWall.Domain.UserEntities;
using StarWall.Domain.WallpaperEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWall.Core.DTOs
{
    public class RegisterUserDTO : LoginUserDTO
    {
        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MaxLength(50, ErrorMessage = "{0} can not be more than {1} characters")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MaxLength(50, ErrorMessage = "{0} can not be more than {1} characters")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter {0}")]
        [EmailAddress(ErrorMessage = "{0} is invalid")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter {0}")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords are not the same!")]
        public string RePassword { get; set; }
    }

    public class LoginUserDTO
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MinLength(5, ErrorMessage = "{0} must be more than {1} characters")]
        [MaxLength(30, ErrorMessage = "{0} can not be more than {2} characters")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MinLength(5, ErrorMessage = "{0} must be more than {1} characters")]
        [MaxLength(30, ErrorMessage = "{0} can not be more than {2} characters")]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{5,30}$", ErrorMessage = "Password must be at least 5 characters, no more than 30 characters, and must include at least 1 upper case letter, 1 lower case letter, and 1 numeric digit.")]
        [DataType(DataType.Password, ErrorMessage = "Invalid Password")]
        public string Password { get; set; }
    }

    public class UserDTO
    {
        public long Id { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MinLength(5, ErrorMessage = "{0} must be more than {1} characters")]
        [MaxLength(30, ErrorMessage = "{0} can not be more than {2} characters")]
        public string? Username { get; set; }

        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MaxLength(50, ErrorMessage = "{0} can not be more than {1} characters")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MaxLength(50, ErrorMessage = "{0} can not be more than {1} characters")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter {0}")]
        [EmailAddress(ErrorMessage = "{0} is invalid")]
        public string Email { get; set; }

        public string? ProfileImage { get; set; }

        public DateTime RegisterDate { get; set; }

        public Role? Role { get; set; }

        public List<Wallpaper>? UploadedWallpapers { get; set; }

        public List<Wallpaper>? DownloadedWallpapers { get; set; }

        public List<Wallpaper>? LastLikedWallpapers { get; set; }
    }

    public class ResetPasswordDTOInUserPanel
    {
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter {0}")]
        [DataType(DataType.Password, ErrorMessage = "Invalid Password")]
        public string CurrentPassword { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MinLength(5, ErrorMessage = "{0} must be more than {1} characters")]
        [MaxLength(30, ErrorMessage = "{0} can not be more than {2} characters")]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{5,30}$", ErrorMessage = "Password must be at least 5 characters, no more than 30 characters, and must include at least 1 upper case letter, 1 lower case letter, and 1 numeric digit.")]
        [DataType(DataType.Password, ErrorMessage = "Invalid Password")]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm New Password")]
        [Required(ErrorMessage = "Please enter {0}")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords are not the same!")]
        public string ReNewPassword { get; set; }
    }

    public class ResetPasswordDTO
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter {0}")]
        public string? Email { get; set; }

        [Display(Name = "Token")]
        [Required(ErrorMessage = "Please enter {0}")]
        public string Token { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MinLength(5, ErrorMessage = "{0} must be more than {1} characters")]
        [MaxLength(30, ErrorMessage = "{0} can not be more than {2} characters")]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{5,30}$", ErrorMessage = "Password must be at least 4 characters, no more than 8 characters, and must include at least 1 upper case letter, 1 lower case letter, and 1 numeric digit.")]
        [DataType(DataType.Password, ErrorMessage = "Invalid Password")]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm New Password")]
        [Required(ErrorMessage = "Please enter {0}")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords are not the same!")]
        public string ReNewPassword { get; set; }
    }

    public class EmailDTO
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter {0}")]
        [EmailAddress(ErrorMessage = "{0} is invalid")]
        public string Email { get; set; }
    }

    public class UpdateUserDTO
    {
        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MaxLength(50, ErrorMessage = "{0} can not be more than {1} characters")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MaxLength(50, ErrorMessage = "{0} can not be more than {1} characters")]
        public string LastName { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter {0}")]
        [MinLength(5, ErrorMessage = "{0} must be more than {1} characters")]
        [MaxLength(30, ErrorMessage = "{0} can not be more than {2} characters")]
        public string Username { get; set; }

        public string? ProfileImage { get; set; }
    }
}
