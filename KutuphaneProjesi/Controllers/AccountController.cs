using KutuphaneProjesi.Data;
using KutuphaneProjesi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KutuphaneProjesi.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                var mevcutKullanici = await _context.Kullanicilar.FirstOrDefaultAsync(k => k.KullaniciAdi == kullanici.KullaniciAdi || k.Eposta == kullanici.Eposta);
                if (mevcutKullanici != null)
                {
                    TempData["HataMesaji"] = "Bu kullanıcı adı veya e-posta zaten alınmış.";
                    return View(kullanici);
                }

                _context.Kullanicilar.Add(kullanici);
                await _context.SaveChangesAsync();
                TempData["BasariMesaji"] = "Kayıt işlemi başarılı! Lütfen giriş yapınız.";
                return RedirectToAction("Login");
            }
            return View(kullanici);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Kullanici model)
        {
            var kullanici = await _context.Kullanicilar
                .FirstOrDefaultAsync(k => k.Eposta == model.Eposta && k.Sifre == model.Sifre);

            if (kullanici != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, kullanici.KullaniciAdi),
                    new Claim(ClaimTypes.Role, kullanici.Rol),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            TempData["HataMesaji"] = "E-posta veya şifre hatalı.";
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}