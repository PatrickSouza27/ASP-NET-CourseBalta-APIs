using _8__AutenticaEAutorizaIdentityAPI.Data;
using _8__AutenticaEAutorizaIdentityAPI.Extensions;
using _8__AutenticaEAutorizaIdentityAPI.Models;
using _8__AutenticaEAutorizaIdentityAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace _8__AutenticaEAutorizaIdentityAPI.Controllers
{
    [ApiController]
    [Route("categorias")]
    public class CategoriaController : ControllerBase
    {
        [HttpGet("testing")]
        public IActionResult OKEndPoint()
           => Ok();

        [HttpGet("todos")]
        public async Task<IActionResult> GetTodasCategorias([FromServices] ApiDataContext conn)
            => Ok(new ResultDefault<List<Categoria>>(await conn.Categorias.AsNoTracking().ToListAsync()));



        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetInfoCategoriaUniq(int id, [FromServices] ApiDataContext conn)
        {
            var categoriaOpc = await conn.Categorias.FirstOrDefaultAsync(x => x.Id == id);
            return categoriaOpc != null && ModelState.IsValid ? Ok(new ResultDefault<Categoria>(categoriaOpc)) : NotFound(new ResultDefault<Usuario>("Categoria não encontrado!"));
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> AdicionarCategoria([FromServices] ApiDataContext conn, [FromBody] ViewCategoria categoria)
        {
            //User.Identity.Name;

            if (!ModelState.IsValid) return BadRequest(new ResultDefault<Curso>(ModelState.ExtensionMessage()));

            await conn.Categorias.AddAsync(new Categoria(categoria.Name));
            conn.SaveChanges();

            return Ok(categoria);
        }

        
        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> AtualizarCategoria(int id, [FromServices] ApiDataContext conn, [FromBody] ViewCategoria editCategoria)
        {
            var updateCategoria = await conn.Categorias.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (!ModelState.IsValid) return BadRequest(new ResultDefault<Curso>(ModelState.ExtensionMessage()));
            if (updateCategoria == null) return NotFound(new ResultDefault<Categoria>("Categoria não encontrado!"));

            updateCategoria.Editar(editCategoria);

            conn.Update(updateCategoria);
            conn.SaveChanges();

            return Ok(new ResultDefault<Categoria>(updateCategoria));
        }
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> RemoverCategoria(int id, [FromServices] ApiDataContext conn)
        {
            var removeCategoria = await conn.Categorias.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (!ModelState.IsValid) return BadRequest(new ResultDefault<Curso>(ModelState.ExtensionMessage()));
            if (removeCategoria == null) return NotFound(new ResultDefault<Categoria>("Categoria não encontrado!"));

            conn.Categorias.Remove(removeCategoria);
            conn.SaveChanges();
            return Ok("Categoria Removido com suceso!");
        }
    }
}
