using _8__AutenticaEAutorizaIdentityAPI.Data;
using _8__AutenticaEAutorizaIdentityAPI.Extensions;
using _8__AutenticaEAutorizaIdentityAPI.Models;
using _8__AutenticaEAutorizaIdentityAPI.Services;
using _8__AutenticaEAutorizaIdentityAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _8__AutenticaEAutorizaIdentityAPI.Controllers
{
    [ApiController]
    [Route("teste")]
    //[Authorize] // significa que a class controller inteira tem que ter autenticação, para algum metodo não precisa como o Login, usar o [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly TokenService _tokenService;
        public AccountController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }
        //é igual public IAcitonResult Login([FromService] TokenService tokenService)

        [AllowAnonymous] //esse metodo qualquer um pode fazer uma requisição
        [HttpPost("login")]
        public IActionResult Login(ViewLogin login, [FromServices] ApiDataContext conn)
        {
            if (!ModelState.IsValid) return BadRequest(new ResultDefault<Usuario>(ModelState.ExtensionMessage()));

            var usuarioAccount = new CriptografiaHash(conn).VerificarUsuarioExiste(login);
            if (usuarioAccount != null)
            {
                var token = _tokenService.GerarToken(usuarioAccount);
                return Ok(new ResultDefault<string>(token, null));
            }
            return NotFound("Usuario Não Encontrado");
        }

        [Authorize(Roles = "user")] //só vai ser autorizado quem tiver "user" no Claim.Roles, "user" no TokenService
        [HttpGet("user")]
        public IActionResult GetUser() => Ok(User.Identity.Name);

        [Authorize(Roles = "author")] //só vai ser autorizado quem tiver "user" no Claim.Roles, "author" no TokenService
        [HttpGet("author")]
        public IActionResult GetAuthor() => Ok(User.Identity.Name);

        [Authorize(Roles = "admin")] //só vai ser autorizado quem tiver "user" no Claim.Roles, "admin" no TokenService
        [HttpGet("admin")]
        public IActionResult GetAdmin() => Ok(User.Identity.Name);

        //pode tbm colocar mais de uma autorização, exemplo, o usuario precisa ter um claim.Roles "user" + Claim.Roles "authro" para ter acesso

        [HttpGet("adminEauhtor")]
        [Authorize(Roles = "admin")] //nessa ordem
        [Authorize(Roles = "author")]
        public IActionResult GetadminEauhtor() => Ok(User.Identity.Name);

        [HttpGet("userEauhtor")]
        [Authorize(Roles = "admin")] //nessa ordem
        [Authorize(Roles = "author")]
        public IActionResult GetuserEauhtor() => Ok(User.Identity.Name);

    }
}
