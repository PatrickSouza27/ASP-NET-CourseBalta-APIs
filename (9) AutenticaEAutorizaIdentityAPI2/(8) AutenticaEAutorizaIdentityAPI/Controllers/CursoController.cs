using _8__AutenticaEAutorizaIdentityAPI.Data;
using _8__AutenticaEAutorizaIdentityAPI.Extensions;
using _8__AutenticaEAutorizaIdentityAPI.Models;
using _8__AutenticaEAutorizaIdentityAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Text.RegularExpressions;

namespace _8__AutenticaEAutorizaIdentityAPI.Controllers
{
    [ApiController]
    [Route("cursos")]
    public class CursoController : ControllerBase
    {
        [HttpGet("testing")]
        public IActionResult OKEndPoint()
           => Ok();

        [HttpGet("todos")]
        public async Task<IActionResult> GetTodosOsCursos([FromServices] ApiDataContext conn)
            => Ok(new ResultDefault<List<Curso>>(await conn.Cursos.AsNoTracking().ToListAsync()));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetInfoCursoUniq(int id, [FromServices] ApiDataContext conn)
        {
            var cursoOpc = await conn.Cursos.FirstOrDefaultAsync(x => x.IdCurso == id);
            return  cursoOpc != null && ModelState.IsValid ? Ok(new ResultDefault<Curso>(cursoOpc)) : NotFound(new ResultDefault<Usuario>("Curso não encontrado!"));
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> AdicionarCurso([FromServices] ApiDataContext conn, [FromBody] ViewCurso curso)
        {
            if(!ModelState.IsValid) return BadRequest(new ResultDefault<Curso>(ModelState.ExtensionMessage()));


            var categoria = CategoriaVinculada(curso.IdentificadorCategoria, conn);

            if (categoria == null) return NotFound(new ResultDefault<Usuario>("Curso não encontrado!"));

            await conn.Cursos.AddAsync(new Curso(curso.Name, categoria));
            conn.SaveChanges();

            return Ok(curso);
        }

        public Categoria? CategoriaVinculada(string categoria, [FromServices] ApiDataContext conn)
        {
            return int.TryParse(categoria, out int valor) ? conn.Categorias.FirstOrDefault(x => x.Id == valor) : conn.Categorias.FirstOrDefault(x => x.Name == categoria);
        }
        
        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> AtualizarCurso(int id, [FromServices] ApiDataContext conn, [FromBody] ViewCurso curso)
        {
            var updateCourse = await conn.Cursos.AsNoTracking().FirstOrDefaultAsync(x => x.IdCurso == id);

            if (!ModelState.IsValid)
                return BadRequest(new ResultDefault<Curso>(ModelState.ExtensionMessage()));

            if (updateCourse == null) 
                return NotFound(new ResultDefault<Usuario>("Curso não encontrado!"));

            var categoria = CategoriaVinculada(curso.IdentificadorCategoria, conn);

            if (categoria == null) 
                return NotFound(new ResultDefault<Categoria>("Categoria não encontrado!"));

            updateCourse.EditarCurso(curso.Name, categoria);

            conn.Update(updateCourse);
            conn.SaveChanges();

            return Ok(new ResultDefault<Curso>(updateCourse));
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> RemoverCurso(int id, [FromServices] ApiDataContext conn)
        {
            var removeCourse = await conn.Cursos.AsNoTracking().FirstOrDefaultAsync(x => x.IdCurso == id);

            if (!ModelState.IsValid) return BadRequest(new ResultDefault<Curso>(ModelState.ExtensionMessage()));
            if (removeCourse == null) return NotFound(new ResultDefault<Usuario>("Curso não encontrado!"));

            conn.Cursos.Remove(removeCourse);
            conn.SaveChanges();
            return Ok("Curso Removido com sucesso");
        }

    }
}
