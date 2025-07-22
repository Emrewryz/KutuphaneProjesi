using KutuphaneProjesi.Data;
using Microsoft.AspNetCore.Mvc;
using KutuphaneProjesi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace KutuphaneProjesi.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin")] 
    public class YazarlarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public YazarlarController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            
            var yazarlar = await _context.YAZARLAR.ToListAsync();
            return View(yazarlar);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(YAZARLAR yazar)
        {
            if (ModelState.IsValid)
            {
                _context.YAZARLAR.Add(yazar);
                await _context.SaveChangesAsync();
                TempData["BasariMesaji"] = $"'{yazar.AdSoyad}' isimli yazar başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(yazar);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yazar = await _context.YAZARLAR.FindAsync(id);

            if (yazar == null)
            {
                return NotFound();
            }
            return View(yazar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, YAZARLAR yazar)
        {
            if (id != yazar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yazar);
                    await _context.SaveChangesAsync();
                    TempData["BasariMesaji"] = $"'{yazar.AdSoyad}' isimli yazar başarıyla güncellendi.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View(yazar);
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yazar = await _context.YAZARLAR.FirstOrDefaultAsync(m => m.Id == id);

            if (yazar == null)
            {
                return NotFound();
            }
            return View(yazar);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yazar = await _context.YAZARLAR.FirstOrDefaultAsync(m => m.Id == id);

            if (yazar == null)
            {
                return NotFound();
            }
            return View(yazar);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            bool yazarinKitaplariVarMi = await _context.KITAPLAR.AnyAsync(k => k.YazarId == id);

            if (yazarinKitaplariVarMi)
            {
                TempData["HataMesaji"] = "Bu yazarın sisteme kayıtlı kitapları olduğu için silme işlemi gerçekleştirilemedi. Lütfen önce bu yazara ait kitapları silin.";
                return RedirectToAction(nameof(Index));
            }

            var yazar = await _context.YAZARLAR.FindAsync(id);
            if (yazar != null)
            {
                string silinenYazarAdi = yazar.AdSoyad;
                _context.YAZARLAR.Remove(yazar);
                await _context.SaveChangesAsync();
                TempData["BasariMesaji"] = $"'{silinenYazarAdi}' isimli yazar başarıyla silindi.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}