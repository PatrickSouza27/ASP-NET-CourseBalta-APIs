using _8__AutenticaEAutorizaIdentityAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _8__AutenticaEAutorizaIdentityAPI.Services
{
    public class TokenService
    {
        public string GerarToken(Usuario usuario)
        {
            var manipularToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.JWTKey);


            var tokenDescriptior = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new (ClaimTypes.Name, "patrick"),  //User.Identity.Name
                    new (ClaimTypes.Role, "admin"),   //User.IsInRole()
                    new (ClaimTypes.Role, "author"),
                    new ("fruta", "banana")
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };



            var token = manipularToken.CreateToken(tokenDescriptior);
            return manipularToken.WriteToken(token);
        }
    }
}
