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
        public IActionResult ResultTodos([FromServices] AppDbContext conn)
            => Ok(conn.Cursos.AsNoTracking().Include(x=> x.Categoria).ToList());
        [HttpGet("{id:int}")]
        public IActionResult ResultId(int id, [FromServices] AppDbContext conn)
            => Ok(conn.Cursos.AsNoTracking().Include(x => x.Categoria).FirstOrDefault(x => x.IdCurso == id));

        [HttpPost("add")]
        public IActionResult AdicionarCategoria([FromBody] Curso curso, [FromServices] AppDbContext conn)
        {
            conn.Cursos.Add(curso);
            conn.SaveChanges();
            return Ok("Salvado com sucesso! status 200");
        }
        [HttpPut("update/{id:int}")]
        public IActionResult AtualizarCategoria(int id, [FromServices] AppDbContext conn, [FromBody] Curso curso)
        {
            var cursosUpdate = conn.Cursos.Include(x=> x.Categoria).FirstOrDefault(x => x.IdCurso == id);
            if (cursosUpdate == null)
                return NotFound();

            cursosUpdate.IdCurso = id;
            cursosUpdate.Name = curso.Name;
            conn.SaveChanges();
            return Ok(cursosUpdate);
        }
        [HttpDelete("/Delete/{id:int}")]
        public IActionResult DeleteCategoria(int id, [FromServices] AppDbContext conn)
        {
            var cursoRemove = conn.Cursos.FirstOrDefault(x => x.IdCurso == id);
            if (cursoRemove == null) return NotFound();

            conn.Cursos.Remove(cursoRemove);
            conn.SaveChanges();
            return Ok("curso Apagada com sucesso");

        }

    }
}
