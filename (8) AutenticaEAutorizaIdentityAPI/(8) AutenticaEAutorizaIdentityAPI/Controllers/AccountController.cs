using _8__AutenticaEAutorizaIdentityAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace _8__AutenticaEAutorizaIdentityAPI.Controllers
{
    [ApiController]
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
        public IActionResult Login()
        {
            var token = _tokenService.GerarToken(null);
            return Ok(token);
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
