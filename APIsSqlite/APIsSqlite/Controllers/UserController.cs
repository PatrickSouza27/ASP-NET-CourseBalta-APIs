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
        public List<User> Get([FromServices] AppDataContext context)
        {
            return context.Users.ToList();
        }

        [HttpPost("/Adicionar")]
        public void Post([FromBody] User user, [FromServices] AppDataContext context)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
        [HttpGet("/{id:int}")]
        public User GetUser(int id, [FromServices] AppDataContext context)
        {
            return context.Users.FirstOrDefault(x => x.Id == id);
        }

        [HttpPut("/Update/{id:int}")]
        public User Put([FromRoute] int id, [FromBody] User user, [FromServices] AppDataContext context)
        {
            var userupdate = context.Users.FirstOrDefault(x => x.Id == id);
            if (userupdate != null)
            {
                userupdate.Id = id;
                userupdate.Name = user.Name;
                userupdate.Login = user.Login;
                userupdate.Senha = user.Senha;
            }
            context.Users.Update(userupdate);
            context.SaveChanges();
            return userupdate;
        }
        [HttpDelete("/Delete/{id:int}")]
        public void Delete([FromRoute] int id, [FromServices] AppDataContext context)
        {
            context.Users.Remove(context.Users.FirstOrDefault(x => x.Id == id));
            context.SaveChanges();
        }
    }
}
