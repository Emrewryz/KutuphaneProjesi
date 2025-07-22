using KutuphaneProjesi.Data;
using KutuphaneProjesi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneProjesi.Controllers
{
    [Authorize(Roles = "Admin")] 
    public class KullaniciYonetimiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KullaniciYonetimiController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var kullanicilar = await _context.Kullanicilar.ToListAsync();
            return View(kullanicilar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoluAdminYap(int id)
        {
            var kullanici = await _context.Kullanicilar.FindAsync(id);
            if (kullanici != null)
            {
                kullanici.Rol = "Admin";
                await _context.SaveChangesAsync();
                TempData["BasariMesaji"] = $"'{kullanici.KullaniciAdi}' kullanıcısı Admin olarak atandı.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoluUyeYap(int id)
        {
            var kullanici = await _context.Kullanicilar.FindAsync(id);
            if (kullanici != null)
            {
                kullanici.Rol = "Uye";
                await _context.SaveChangesAsync();
                TempData["BasariMesaji"] = $"'{kullanici.KullaniciAdi}' kullanıcısı Üye olarak atandı.";
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var kullanici = await _context.Kullanicilar.FirstOrDefaultAsync(m => m.Id == id);
            if (kullanici == null) return NotFound();
            return View(kullanici);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kullanici = await _context.Kullanicilar.FindAsync(id);
            if (kullanici != null)
            {
                _context.Kullanicilar.Remove(kullanici);
                await _context.SaveChangesAsync();
                TempData["BasariMesaji"] = "Kullanıcı başarıyla silindi.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}