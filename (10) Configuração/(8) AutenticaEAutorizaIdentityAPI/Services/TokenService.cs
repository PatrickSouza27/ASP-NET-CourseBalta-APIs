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
                    new (ClaimTypes.Name, usuario.Name),  //User.Identity.Name  //Esse claim tem que ser o Login por ser unico ou o Email
                    //new (ClaimTypes.Role, "admin"),   //User.IsInRole()
                    //new (ClaimTypes.Role, "author"),
                    //new ("fruta", "banana")
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            usuario.RolesUser.ForEach(x => tokenDescriptior.Subject.AddClaim(new Claim(ClaimTypes.Role, x.Name)));
            //ta dando duplicação pq toda vez ta sendo gerado um new Roles lá, o Roles User name tem que ser unico tbm, assim como o login


            var token = manipularToken.CreateToken(tokenDescriptior);
            return manipularToken.WriteToken(token);
        }
    }
}
