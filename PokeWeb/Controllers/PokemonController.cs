using Microsoft.AspNetCore.Mvc;
using PokeWeb.Models;
using static PokeWeb.Models.DbModel;

namespace PokeWeb.Controllers
{
    public class PokemonController : Controller
    {
        private readonly PokeContext _db;
        private readonly IWebHostEnvironment _env;
        public PokemonController(PokeContext context, IWebHostEnvironment env)
        {
            _db = context;
            _env = env;
        }
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
        [HttpPost]
        public async Task<IActionResult> PokemonType(PokemonType_Website ptw)
        {
            string path = _env.WebRootPath + @"\image\Pokemon\";
            db_PokemonType dpt = new db_PokemonType();
            dpt.No = _db.PokemonTypes.Count() + 1;
            dpt.TwName = ptw.Type.TwName;
            dpt.JpName = ptw.Type.JpName;
            dpt.EnName = ptw.Type.EnName;
            dpt.Image = "~/image/Pokemon/" + ptw.Type.Image[0].FileName;
            dpt.ImageIcon = "~/image/Pokemon/" + ptw.Type.ImageIcon[0].FileName;
            try
            {
                if (ptw.Type.Image[0].Length > 0)
                {
                    using (var stream = new FileStream(path + ptw.Type.Image[0].FileName, FileMode.Create))
                    {
                        await ptw.Type.Image[0].CopyToAsync(stream);
                    }
                }
                if (ptw.Type.ImageIcon[0].Length > 0)
                {
                    using (var stream = new FileStream(path + ptw.Type.ImageIcon[0].FileName, FileMode.Create))
                    {
                        await ptw.Type.ImageIcon[0].CopyToAsync(stream);
                    }
                }
                await _db.PokemonTypes.AddAsync(dpt);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("PokemonType");
        }
    }
}
