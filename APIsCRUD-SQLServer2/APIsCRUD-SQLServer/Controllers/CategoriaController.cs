using APIsCRUD_SQLServer.Data;
using APIsCRUD_SQLServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System.Reflection.Metadata.Ecma335;

namespace APIsCRUD_SQLServer.Controllers
{
    [ApiController]
    [Route("categorias")]
    public class CategoriaController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult TestingOk()
            => Ok();
        //[HttpGet("todos-usuarios")] //você pode usar dois EndPoints que chega no mesmo lugar
        [HttpGet("todos")] //nomear EndPoint sempre no minusculo e sempre no plural
        public async Task<IActionResult> ResultTodosAsync([FromServices] AppDbContext conn)
            => Ok(await conn.Categorias.ToListAsync());

        //melhor jeito de versionar a api é colocar assim ("v1/todos") apps que não foram atualizado continuara funcionando na versão 1
        //melhor jeito de versionar a api é colocar assim ("v2/todos") enquanto os apps que ja foram atualizado funcionara na 2
        //-------------------------------------------------------------------------------------------------------------------------\\
        [HttpGet("{id:int}")]
        public async Task<IActionResult> ResultIdAsync(int id, [FromServices] AppDbContext conn)
            => Ok(await conn.Categorias.AsNoTracking().FirstOrDefaultAsync(x=> x.Id == id));

        [HttpPost("add")]
        public async Task<IActionResult> AdicionarCategoriaAsync([FromBody] Categoria categoria, [FromServices] AppDbContext conn)
        {
            await conn.Categorias.AddAsync(categoria);
            await conn.SaveChangesAsync();
            return Created("Salvado com sucesso! status 200", categoria);
        }
        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> AtualizarCategoriaAsync(int id, [FromServices] AppDbContext conn, [FromBody] Categoria categoria)
        {
            var categoriaUpdate = await conn.Categorias.FirstOrDefaultAsync(x => x.Id == id);
            if (categoriaUpdate == null)
                return NotFound();

            categoriaUpdate.Name = categoria.Name;
            conn.Categorias.Update(categoriaUpdate);
            await conn.SaveChangesAsync();
            return Ok($"Atualizado com sucesso");
        }
        [HttpDelete("delete/{id:int}")]
        public IActionResult DeleteCategoria(int id, [FromServices] AppDbContext conn)
        {
            var categoriaRemove = conn.Categorias.FirstOrDefault(x=> x.Id == id);
            if(categoriaRemove == null) return NotFound();



            conn.Categorias.Remove(categoriaRemove);
            conn.SaveChanges();
            return Ok("Categoria Apagada com sucesso");

        }

    }       
}
