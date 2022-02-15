using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            ViewBag.PokemonList = (from x in _db.Set<db_Pokemon>()
                                   join y in _db.Set<db_PokemonType>() on x.Type_1 equals y.No
                                   join z in _db.Set<db_PokemonType>() on x.Type_2 equals z.No
                                   select new PokemonList()
                                   {
                                       No = x.No,
                                       TwName = x.TwName,
                                       JpName = x.JpName,
                                       EnName = x.EnName,
                                       Type_1 = string.IsNullOrEmpty(y.TwName) ? "" : y.TwName,
                                       Type_1_Img = string.IsNullOrEmpty(y.Image) ? "" : y.Image,
                                       Type_2 = string.IsNullOrEmpty(z.TwName) ? "" : z.TwName,
                                       Type_2_Img = string.IsNullOrEmpty(z.Image) ? "" : z.Image
                                   }).ToList();
            return View();
        }

        public IActionResult PokemonAdd()
        {
            PokemonAdd_Website pw = new PokemonAdd_Website();
            ViewBag.PokeType = new SelectList(from x in _db.Set<db_PokemonType>()
                                              select new { x.No, x.TwName }, "No", "TwName");
            int NewNo = 0;
            bool intchg = int.TryParse((from x in _db.Set<db_Pokemon>()
                                        orderby x.No descending
                                        select x.No).FirstOrDefault(), out NewNo);
            db_Pokemon pm = new db_Pokemon();
            if (intchg)
            {
                NewNo = NewNo + 1;
                string PokeNo = "0000" + NewNo;
                pm.No = PokeNo.Substring(PokeNo.Length - 4, 4);
            }
            else
            {
                pm.No = "0001";
            }
            pw.Pokemon = pm;
            return View(pw);
        }

        [HttpPost]
        public async Task<IActionResult> PokemonAdd(PokemonAdd_Website pw)
        {
            try
            {
                pw.Pokemon.CreatTime = DateTime.Now;
                await _db.Pokemons.AddAsync(pw.Pokemon);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("PokemonAdd");
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
