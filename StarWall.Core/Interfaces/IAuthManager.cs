using StarWall.Domain.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWall.Core.Interfaces
{
    public interface IAuthManager
    {
        string GenerateJWTToken(User user);
    }
}
