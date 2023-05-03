using APIsCRUD_SQLServer.Data;
using APIsCRUD_SQLServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIsCRUD_SQLServer.Controllers
{
    [ApiController]
    [Route("/cursos")]
    public class CursosController : ControllerBase
    {
        [HttpGet("")]

        //To informando se a API está online ou offline (anotar)
        public IActionResult TestingOk()
            => Ok();

        [HttpGet("todos")]
        public async Task<IActionResult> ResultTodosAsync([FromServices] AppDbContext conn)
            => Ok(await conn.Cursos.AsNoTracking().Include(x=> x.Categoria).ToListAsync());
        [HttpGet("{id:int}")]
        public async Task<IActionResult> ResultIdAsync(int id, [FromServices] AppDbContext conn)
            => Ok(await conn.Cursos.AsNoTracking().Include(x => x.Categoria).FirstOrDefaultAsync(x => x.IdCurso == id));

        [HttpPost("add")]
        public async Task<IActionResult> AdicionarCategoriaAsync([FromBody] Curso curso, [FromServices] AppDbContext conn)
        {
            await conn.Cursos.AddAsync(curso);
            await conn.SaveChangesAsync();
            return Ok("Salvado com sucesso! status 200");
        }
        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> AtualizarCategoriaAsync(int id, [FromServices] AppDbContext conn, [FromBody] Curso curso)
        {
            var cursosUpdate = await conn.Cursos.Include(x=> x.Categoria).FirstOrDefaultAsync(x => x.IdCurso == id);
            if (cursosUpdate == null)
                return NotFound();

            cursosUpdate.IdCurso = id;
            cursosUpdate.Name = curso.Name;
            await conn.SaveChangesAsync();
            return Ok(cursosUpdate);
        }
        [HttpDelete("/Delete/{id:int}")]
        public async Task<IActionResult> DeleteCategoria(int id, [FromServices] AppDbContext conn)
        {
            var cursoRemove = await conn.Cursos.FirstOrDefaultAsync(x => x.IdCurso == id);
            if (cursoRemove == null) return NotFound();

            conn.Cursos.Remove(cursoRemove);
            await conn.SaveChangesAsync();
            return Ok("curso Apagada com sucesso");

        }

    }
}
