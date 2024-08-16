using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWall.Core.Models
{
    public class DecodedJWTModel
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public long Id { get; set; }
    }
}
