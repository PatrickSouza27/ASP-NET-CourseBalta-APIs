using _8__AutenticaEAutorizaIdentityAPI.Attributes;
using _8__AutenticaEAutorizaIdentityAPI.Data;
using _8__AutenticaEAutorizaIdentityAPI.Extensions;
using _8__AutenticaEAutorizaIdentityAPI.Models;
using _8__AutenticaEAutorizaIdentityAPI.Services;
using _8__AutenticaEAutorizaIdentityAPI.ViewModel;
using _8__AutenticaEAutorizaIdentityAPI.ViewModel.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _8__AutenticaEAutorizaIdentityAPI.Controllers
{
    [ApiController]
    [Route("")]
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

        [HttpGet("teste")]
        [ApiKey]
        public IActionResult GetApiKey() => Ok("TESTE");


        [Authorize]
        [HttpPost("image")]
        public async Task<IActionResult> UploadImage([FromBody] ViewImage imagem, [FromServices] ApiDataContext data)
        {
            var fileName = $"{Guid.NewGuid()}.jpg";
            var imagemdata = new Regex(@"^data:image [a-z]+;base64").Replace(imagem.Image64Base, "");
            var bytes = Convert.FromBase64String(imagemdata);

            try
            {
                await System.IO.File.WriteAllBytesAsync($"wwwroot/imagens/{fileName}", bytes);

            }catch(Exception ex)
            {
                return StatusCode(500, new ResultDefault<string>("05X04 - Falha Interna no Servidor"));
            }

            //var user = await data.Usuarios.FirstOrDefaultAsync(x => x.Name == User.Identity.Name);

            //if (user != null)
            //    return NotFound(new ResultDefault<Usuario>("Usuario não Encontrado"));

            return Ok("imagem salva");


        }
        

    }
}
