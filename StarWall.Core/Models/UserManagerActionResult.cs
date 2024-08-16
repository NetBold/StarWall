using StarWall.Domain.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWall.Core.Models
{
    public class UserManagerActionResult
    {
        public User? User { get; set; }
        public string? Token { get; set; }
        public string? ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
