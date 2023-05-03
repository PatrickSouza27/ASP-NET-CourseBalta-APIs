using APIsCRUD_SQLServer.Data;
using APIsCRUD_SQLServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System.Reflection.Metadata.Ecma335;

namespace APIsCRUD_SQLServer.Controllers
{
    [ApiController]
    [Route("/categoria")]
    public class CategoriaController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult TestingOk()
            => Ok();

        [HttpGet("todos")]
        public IActionResult ResultTodos([FromServices] AppDbContext conn)
            => Ok(conn.Categorias.ToList());


        [HttpGet("{id:int}")]
        public IActionResult ResultId(int id, [FromServices] AppDbContext conn)
            => Ok(conn.Categorias.AsNoTracking().FirstOrDefault(x=> x.Id == id));

        [HttpPost("add")]
        public IActionResult AdicionarCategoria([FromBody] Categoria categoria, [FromServices] AppDbContext conn)
        {
            conn.Categorias.Add(categoria);
            conn.SaveChanges();
            return Ok("Salvado com sucesso! status 200");
        }
        [HttpPut("update/{id:int}")]
        public IActionResult AtualizarCategoria(int id, [FromServices] AppDbContext conn, [FromBody] Categoria categoria)
        {
            var categoriaUpdate = conn.Categorias.FirstOrDefault(x => x.Id == id);
            if (categoriaUpdate == null)
                return NotFound();

            categoriaUpdate.Id = id;
            categoriaUpdate.Name = categoria.Name;
            conn.SaveChanges();
            return Ok(categoriaUpdate);
        }
        [HttpDelete("/Delete/{id:int}")]
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
