using Microsoft.AspNetCore.Mvc;

namespace CRUD_APIs_SqlSeverMigrations.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        public IActionResult HelpCheck()
            => Ok();
    }
}
