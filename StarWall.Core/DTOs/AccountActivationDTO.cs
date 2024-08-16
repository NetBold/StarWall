using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWall.Core.DTOs
{
    public class AccountActivationDTO
    {
        public string Username { get; set; }

        [Display(Name = "Token")]
        [Required(ErrorMessage = "enter the {0}")]
        public string UserToeken { get; set; }
    }
}
