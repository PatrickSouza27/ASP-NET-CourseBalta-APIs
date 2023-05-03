using Blog.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRUD_APIs_SqlSeverMigrations.Services
{
    public class TokenService
    {
        public string GeneratedToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.JWTKey);
            var tokenDescript = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new (ClaimTypes.Name, "Patrick"), //User.Identity.Name
                    new (ClaimTypes.Role, "admin"),   //User.IsInRole
                    new ("Fruta", "banana")
                }),             
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescript);
            return tokenHandler.WriteToken(token);

        }
    }
}
