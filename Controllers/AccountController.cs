using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace HediyelikEsya.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // ŞİFREYİ BURADAN DEĞİŞTİREBİLİRSİN
            if (username == "buga0305" && password == "16008124036") 
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, "Admin") };
                var identity = new ClaimsIdentity(claims, "CookieAuth");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("CookieAuth", principal);
                return RedirectToAction("Index", "Urunler");
            }

            ViewBag.Hata = "Kullanıcı adı veya şifre yanlış!";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Home");
        }
    }
}