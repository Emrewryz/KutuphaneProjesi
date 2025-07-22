using KutuphaneProjesi.Data;
using KutuphaneProjesi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneProjesi.Controllers
{
    [Authorize(Roles = "Admin")] 
    public class KategorilerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KategorilerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Kategoriler.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kategori);
                await _context.SaveChangesAsync();
                TempData["BasariMesaji"] = $"'{kategori.Ad}' kategorisi başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(kategori);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var kategori = await _context.Kategoriler.FindAsync(id);
            if (kategori == null) return NotFound();
            return View(kategori);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Kategori kategori)
        {
            if (id != kategori.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategori);
                    await _context.SaveChangesAsync();
                    TempData["BasariMesaji"] = $"'{kategori.Ad}' kategorisi başarıyla güncellendi.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(kategori);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var kategori = await _context.Kategoriler.FirstOrDefaultAsync(m => m.Id == id);
            if (kategori == null) return NotFound();
            return View(kategori);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool kategorideKitapVarMi = await _context.KITAPLAR.AnyAsync(k => k.KategoriId == id);
            if (kategorideKitapVarMi)
            {
                TempData["HataMesaji"] = "Bu kategoriye atanmış kitaplar olduğu için silinemedi.";
                return RedirectToAction(nameof(Index));
            }

            var kategori = await _context.Kategoriler.FindAsync(id);
            if (kategori != null)
            {
                _context.Kategoriler.Remove(kategori);
                await _context.SaveChangesAsync();
                TempData["BasariMesaji"] = "Kategori başarıyla silindi.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}