using CRUD_APIs_SqlSeverMigrations.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_APIs_SqlSeverMigrations.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly TokenService _tokenService;
        public AccountController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }
        
        [HttpPost("v1/login")]
        public IActionResult Login([FromServices] TokenService tokenService)
        {
            var token = tokenService.GeneratedToken(null);
            return Ok(token);
        }
    }
}
