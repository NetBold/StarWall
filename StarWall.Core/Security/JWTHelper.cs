using StarWall.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StarWall.Core.Security
{
    public static class JWTHelper
    {
        public static DecodedJWTModel GetDecodedJWTModelByToken(string token)
        {
            if (CheckTokenIsValid(token))
            {
                var tokenHandler = new JwtSecurityToken(token);
                var claims = tokenHandler.Claims;
                string username = claims.SingleOrDefault(x => x.Type == "Username").Value;
                string role = claims.SingleOrDefault(x => x.Type == "roles").Value;
                long id = Convert.ToInt64(claims.SingleOrDefault(x => x.Type == "Id").Value);
                var tokenExp = claims.First(claim => claim.Type.Equals("exp")).Value;

                DecodedJWTModel decodedJWTModel = new()
                {
                    Username = username,
                    Role = role,
                    Id = id
                };
                return decodedJWTModel;
            }
            return new DecodedJWTModel();
        }

        public static long GetTokenExpirationTime(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var tokenExp = jwtSecurityToken.Claims.First(claim => claim.Type.Equals("exp")).Value;
            var ticks = long.Parse(tokenExp);
            return ticks;
        }

        public static bool CheckTokenIsValid(string token)
        {
            var tokenTicks = GetTokenExpirationTime(token);
            var tokenDate = DateTimeOffset.FromUnixTimeSeconds(tokenTicks).UtcDateTime;

            var now = DateTime.Now.ToUniversalTime();

            var valid = tokenDate >= now;

            return valid;
        }
    }
}
