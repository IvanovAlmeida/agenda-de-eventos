using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AgendaDeEventos.Application.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index() => View();
    }
}