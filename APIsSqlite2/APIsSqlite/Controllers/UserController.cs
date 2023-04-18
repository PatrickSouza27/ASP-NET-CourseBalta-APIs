using APIsSqlite.Data;
using APIsSqlite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace APIsSqlite.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult Get([FromServices] AppDataContext context)
            => Ok(context.Users.ToList());


        [HttpPost("/Adicionar")]
        public ActionResult Post([FromBody] User user, [FromServices] AppDataContext context)
        {
            context.Users.Add(user);
            context.SaveChanges();

            return Created($"/{user.Id}", user);
        }

        [HttpGet("/{id:int}")]
        public ActionResult GetUser(int id, [FromServices] AppDataContext context)
        {
            var userGet = context.Users.FirstOrDefault(x => x.Id == id);
            return (userGet == null) ? NotFound() : Ok(userGet);
        }

        [HttpPut("/Update/{id:int}")]
        public ActionResult Put([FromRoute] int id, [FromBody] User user, [FromServices] AppDataContext context)
        {
            var userupdate = context.Users.FirstOrDefault(x => x.Id == id);
            if (userupdate == null)
                return NotFound();

            userupdate.Id = id;
            userupdate.Name = user.Name;
            userupdate.Login = user.Login;
            userupdate.Senha = user.Senha;

            context.Users.Update(userupdate);
            context.SaveChanges();
            return Ok(userupdate);
        }

        [HttpDelete("/Delete/{id:int}")]
        public void Delete([FromRoute] int id, [FromServices] AppDataContext context)
        {
            context.Users.Remove(context.Users.FirstOrDefault(x => x.Id == id));
            context.SaveChanges();
        }
    }
}
