using _8__AutenticaEAutorizaIdentityAPI.Data;
using _8__AutenticaEAutorizaIdentityAPI.Extensions;
using _8__AutenticaEAutorizaIdentityAPI.Models;
using _8__AutenticaEAutorizaIdentityAPI.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace _8__AutenticaEAutorizaIdentityAPI.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet("testing")]
        public IActionResult OKEndPoint()
           => Ok();

        [HttpGet("todos")]
        public async Task<IActionResult> GetTodosUsuarios([FromServices] ApiDataContext conn)
            => Ok(new ResultDefault<List<Usuario>>(await conn.Usuarios.AsNoTracking().ToListAsync()));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetInfoUsuarioUniq(int id, [FromServices] ApiDataContext conn)
        {
            var UsuarioOpc = await conn.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            return UsuarioOpc != null && ModelState.IsValid ? Ok(new ResultDefault<Usuario>(UsuarioOpc)) : NotFound(new ResultDefault<Usuario>("Usuário não encontrado!"));
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> AdicionarCurso([FromServices] ApiDataContext conn, [FromBody] ViewUsuario user)
        {
            if (!ModelState.IsValid) return BadRequest(new ResultDefault<Usuario>(ModelState.ExtensionMessage()));

            var passwordHasher = new PasswordHasher<ViewUsuario>();
            var hashedPassword = passwordHasher.HashPassword(user, user.Password);   ///criptografa senha, no caso da senha se passa o hashedPassword
            //Lembre-se de que você precisa usar o método VerifyHashedPassword em vez de HashPassword durante o processo de login para verificar se a senha informada pelo usuário é válida.

            await conn.Usuarios.AddAsync(new Usuario(user.Name, user.Login, hashedPassword));
            conn.SaveChanges();

            return Created("usuario salvo", user);
        }

        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> AtualizarCurso(int id, [FromServices] ApiDataContext conn, [FromBody] ViewUsuario user)
        {
            var updateUsuario = await conn.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (!ModelState.IsValid) return BadRequest(new ResultDefault<Usuario>(ModelState.ExtensionMessage()));
            if (updateUsuario == null) return NotFound(new ResultDefault<Usuario>("Usuário não encontrado!"));

            updateUsuario.UpdateUser(user);


            conn.Usuarios.Update(updateUsuario);
            await conn.SaveChangesAsync();
            return Ok(new ResultDefault<Usuario>(updateUsuario));
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> RemoverCurso(int id, [FromServices] ApiDataContext conn)
        {
            var removeUsuario = await conn.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (!ModelState.IsValid) return BadRequest(new ResultDefault<Usuario>(ModelState.ExtensionMessage()));
            if (removeUsuario == null) return NotFound(new ResultDefault<Usuario>("Usuário não encontrado!"));

            conn.Usuarios.Remove(removeUsuario);
            conn.SaveChanges();
            return Ok("Usuario Removido com sucesso");


        }
    }
}
