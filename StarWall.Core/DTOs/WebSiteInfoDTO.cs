using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWall.Core.DTOs
{
    public class WebSiteInfoDTO
    {
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Enter the {0}")]
        [MinLength(5, ErrorMessage = "{0} must be more than {1} characters")]
        [MaxLength(23, ErrorMessage = "{0} can not be more than {2} characters")]
        public string? Title { get; set; }

        [Display(Name = "Phone Number 1")]
        [Required(ErrorMessage = "Enter the {0}")]
        [StringLength(11,ErrorMessage ="{0} must be {1} characters")]
        public string? PhoneNumber1 { get; set; }

        [Display(Name = "Phone Number 2")]
        [Required(ErrorMessage = "Enter the {0}")]
        [StringLength(11, ErrorMessage = "{0} must be {1} characters")]
        public string? PhoneNumber2 { get; set; }

        [Display(Name = "Short Description")]
        [Required(ErrorMessage = "Enter the {0}")]
        [MinLength(20, ErrorMessage = "{0} must be more than {1} characters")]
        [MaxLength(130, ErrorMessage = "{0} can not be more than {2} characters")]
        public string? ShortDescription { get; set; }

        [Display(Name = "Long Description")]
        [Required(ErrorMessage = "Enter the {0}")]
        [MinLength(20, ErrorMessage = "{0} must be more than {1} characters")]
        public string? LongDescription { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Enter the {0}")]
        public string? Address { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Enter the {0}")]
        [EmailAddress(ErrorMessage = "{0} is invalid")]
        public string? Email { get; set; }

        [Display(Name = "Powered By")]
        [Required(ErrorMessage = "Enter the {0}")]
        public string? PoweredBy { get; set; }

        [Display(Name = "Telegram")]
        public string? Telegram { get; set; }

        [Display(Name = "Instagram")]
        public string? Instagram { get; set; }

        [Display(Name = "Twitter")]
        public string? Twitter { get; set; }
    }

    public class UpdateWebSiteInfoDTO
    {
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Enter the {0}")]
        [MinLength(5, ErrorMessage = "{0} must be more than {1} characters")]
        [MaxLength(23, ErrorMessage = "{0} can not be more than {2} characters")]
        public string? Title { get; set; }

        [Display(Name = "Phone Number 1")]
        [Required(ErrorMessage = "Enter the {0}")]
        [StringLength(11, ErrorMessage = "{0} must be {1} characters")]
        public string? PhoneNumber1 { get; set; }

        [Display(Name = "Phone Number 2")]
        [Required(ErrorMessage = "Enter the {0}")]
        [StringLength(11, ErrorMessage = "{0} must be {1} characters")]
        public string? PhoneNumber2 { get; set; }

        [Display(Name = "Short Description")]
        [Required(ErrorMessage = "Enter the {0}")]
        [MinLength(20, ErrorMessage = "{0} must be more than {1} characters")]
        [MaxLength(130, ErrorMessage = "{0} can not be more than {2} characters")]
        public string? ShortDescription { get; set; }

        [Display(Name = "Long Description")]
        [Required(ErrorMessage = "Enter the {0}")]
        [MinLength(20, ErrorMessage = "{0} must be more than {1} characters")]
        public string? LongDescription { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Enter the {0}")]
        public string? Address { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Enter the {0}")]
        [EmailAddress(ErrorMessage = "{0} is invalid")]
        public string? Email { get; set; }

        [Display(Name = "Powered By")]
        [Required(ErrorMessage = "Enter the {0}")]
        public string? PoweredBy { get; set; }

        [Display(Name = "Telegram")]
        public string? Telegram { get; set; }

        [Display(Name = "Instagram")]
        public string? Instagram { get; set; }

        [Display(Name = "Twitter")]
        public string? Twitter { get; set; }
    }
}
