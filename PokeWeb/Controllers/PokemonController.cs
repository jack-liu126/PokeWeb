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
        private string _staticRoute(string FileRoute) => _env.WebRootPath + "\\" + FileRoute;
        private string _dbRoute(string FileRoute) => "~/" + FileRoute;
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
                                       Type_2_Img = string.IsNullOrEmpty(z.Image) ? "" : z.Image,
                                       Image = string.IsNullOrEmpty(x.ImgRoute) ? "" : x.ImgRoute
                                   }).ToList();
            return View();
        }

        public IActionResult PokemonAdd()
        {
            //ViewBag.PokeType = new SelectList(await _db.db_PokemonType.OrderBy(x => x.No).ToListAsync(), "No", "TwName");
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
                string FileName = pw.Pokemon.No + "_" + pw.Pokemon.TwName;
                pw.Pokemon.CreatTime = DateTime.Now;
                pw.Pokemon.ImgRoute = _dbRoute("image/Pokemon/Pokemon/") + FileName + Path.GetExtension(pw.Pokemon.ImgFile.FileName);
                SaveFile(pw.Pokemon.ImgFile, _staticRoute("image\\Pokemon\\Pokemon\\"), FileName + Path.GetExtension(pw.Pokemon.ImgFile.FileName));
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
            string path = _staticRoute("image/Pokemon/Type/");
            db_PokemonType dpt = new db_PokemonType();
            dpt.No = _db.PokemonTypes.Count() + 1;
            dpt.TwName = ptw.Type.TwName;
            dpt.JpName = ptw.Type.JpName;
            dpt.EnName = ptw.Type.EnName;
            dpt.Image = _dbRoute("image/Pokemon/Type/") + ptw.Type.Image[0].FileName;
            dpt.ImageIcon = _dbRoute("image/Pokemon/Type/") + ptw.Type.ImageIcon[0].FileName;
            try
            {
                if (ptw.Type.Image[0].Length > 0)
                {
                    SaveFile(ptw.Type.Image[0], path);
                }
                if (ptw.Type.ImageIcon[0].Length > 0)
                {
                    SaveFile(ptw.Type.ImageIcon[0], path);
                }
                await _db.PokemonTypes.AddAsync(dpt);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("PokemonType");
        }

        public IActionResult PokemonEdit(string id)
        {
            PokemonEdit_Website pw=new PokemonEdit_Website();
            PokemonList pl = (from x in _db.Set<db_Pokemon>()
                              where x.No == id
                              select new PokemonList()
                              {
                                  No = x.No,
                                  TwName = x.TwName,
                                  EnName = x.EnName,
                                  JpName = x.JpName,
                                  Image = string.IsNullOrEmpty(x.ImgRoute) ? "" : x.ImgRoute,
                                  Type_1 = x.Type_1.ToString(),
                                  Type_2 = x.Type_2.ToString()
                              }).First();
            pw.PokemonList = pl;
            ViewBag.PokeType = new SelectList(from x in _db.Set<db_PokemonType>()
                                              select new { x.No, x.TwName }, "No", "TwName");
            return View(pw);
        }

        [HttpPost]
        public async Task<IActionResult> PokemonEdit(PokemonEdit_Website pw)
        {
            try
            {
                db_Pokemon db_p = new db_Pokemon();
                db_p.No = pw.PokemonList.No;
                db_p.TwName = pw.PokemonList.TwName;
                db_p.JpName = pw.PokemonList.JpName;
                db_p.EnName = pw.PokemonList.EnName;
                db_p.Type_1=Convert.ToInt32(pw.PokemonList.Type_1);
                db_p.Type_2=Convert.ToInt32(pw.PokemonList.Type_2);
                db_p.ImgFile = pw.PokemonList.ImageFile;
                string FileName = db_p.No + "_" + db_p.TwName;
                db_p.CreatTime = DateTime.Now;
                db_p.ImgRoute = _dbRoute("image/Pokemon/Pokemon/") + FileName + Path.GetExtension(db_p.ImgFile.FileName);
                SaveFile(db_p.ImgFile, _staticRoute("image\\Pokemon\\Pokemon\\"), FileName + Path.GetExtension(db_p.ImgFile.FileName));
                _db.Pokemons.Update(db_p);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Detail");
        }

        public async void SaveFile(IFormFile file, string Path)
        {
            if (file != null)
            {
                if (file.Length > 0)
                {
                    using FileStream stream = new FileStream(Path + file.FileName, FileMode.Create);
                    await file.CopyToAsync(stream);
                }
            }
        }

        public async void SaveFile(IFormFile file, string Path, string FileName)
        {
            if (file != null)
            {
                if (file.Length > 0)
                {
                    using FileStream stream = new FileStream(Path + FileName, FileMode.Create);
                    await file.CopyToAsync(stream);
                }
            }
        }
    }
}
