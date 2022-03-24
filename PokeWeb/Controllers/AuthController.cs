using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokeWeb.Models;
using System.Security.Claims;
using static PokeWeb.Models.DbModel;

namespace PokeWeb.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly PokeContext _db;

        public AuthController(PokeContext db)
        {
            _db = db;
        }

        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginPost login)
        {
            var user = await (from x in _db.Set<db_Employee>()
                              where x.Account == login.Account && x.Password == login.Password
                              select x).SingleOrDefaultAsync();

            if(user == null)
            {
                ViewBag.Error = "帳號密碼錯誤";
                return View();
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Account),
                    new Claim("FullName", user.Nick),
                    //new Claim(ClaimTypes.Role, "Administrator")
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return Redirect("/Pokemon/Index");
            }
        }
        
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Auth/Login");
        }
    }
}
