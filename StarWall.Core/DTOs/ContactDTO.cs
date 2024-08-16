using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWall.Core.DTOs
{
    public class ContactDTO
    {
        public long Id { get; set; }

        [Display(Name ="FullName")]
        [Required(ErrorMessage = "Enter {0}")]
        [MaxLength(110,ErrorMessage ="{0} can not be more than {1} characters")]
        public string FullName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Enter {0}")]
        [EmailAddress(ErrorMessage = "{0} is invalid")]
        public string Email { get; set; }

        [Display(Name = "Body")]
        [Required(ErrorMessage = "Enter the {0}")]
        public string Body { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
