using Microsoft.AspNetCore.Mvc;

namespace PokeWeb.Controllers
{
    public class PokemonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
