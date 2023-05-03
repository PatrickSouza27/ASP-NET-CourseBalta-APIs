using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CRUD_APIs_SqlSeverMigrations.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult HelpCheck()
            => Ok();
        [HttpGet("v1/todos")]
        public async Task<IActionResult> GroupUsers([FromServices] BlogDataContext conn)
            => Ok(await conn.Categories.AsNoTracking().ToListAsync());
        [HttpGet("v1/{id:int}")]
        public async Task<IActionResult> IdUsers(int id, [FromServices] BlogDataContext conn)
            => Ok(await conn.Categories.FirstOrDefaultAsync(x => x.Id == id));

        [HttpPost("v1/adicionar")]
        public async Task<IActionResult> AdicionarCategory([FromServices] BlogDataContext conn, [FromBody] Category category)
        {
            try
            {
                await conn.Categories.AddAsync(category);
                conn.SaveChanges();
                return Created("Salvo com sucesso", category);
            }
            catch (DbException)
            {
                return StatusCode(500, "1XE12 - Não foi possivel incluir a categoria");
            }
            catch (Exception)
            {
                return StatusCode(500, "1XE22 - Não foi possivel incluir a categoria - Falha no Servidor");
            }
        }

        [HttpPut("v1/atualizar/{id:int}")]
        public async Task<IActionResult> Atualizar(int id, [FromServices] BlogDataContext conn, [FromBody] Category category)
        {
            var update = await conn.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (update == null) return NotFound();
            
            update.Name = category.Name;
            update.Slug = category.Slug;
            update.Posts = category.Posts;
            try
            {
                conn.Categories.Update(update);
                conn.SaveChanges();

                return Ok("Atualizado com sucesso");
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "45XC21 - Erro ao Atualizar");
            }
            catch (Exception)
            {
                return StatusCode(500, "45XC21 - Falha no Servidor");
            }
        }
        [HttpDelete("v1/delete/{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id, [FromServices] BlogDataContext conn)
        {
            var remove = await conn.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (remove == null) return NotFound();

            conn.Categories.Remove(remove);
            conn.SaveChanges();
            return Ok("DELETADO");
        }

    }
}
