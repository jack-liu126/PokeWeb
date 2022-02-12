using Microsoft.AspNetCore.Mvc;

namespace PokeWeb.Controllers
{
    public class PokemonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult PokemonAdd()
        {
            return View();
        }

        public IActionResult PokemonType()
        {
            return View();
        }
    }
}
