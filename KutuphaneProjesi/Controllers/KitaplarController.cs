using Microsoft.AspNetCore.Mvc;
using KutuphaneProjesi.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using KutuphaneProjesi.Models;
using Microsoft.AspNetCore.Authorization;

namespace KutuphaneProjesi.Controllers
{
    [Authorize]
    public class KitaplarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KitaplarController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? aramakelimesi, int? kategoriId)
        {
            ViewBag.Kategoriler = await _context.Kategoriler.ToListAsync();

            var sorgu = _context.KITAPLAR.Include(k => k.Yazar).Include(k => k.Kategori).AsQueryable();

            if (!string.IsNullOrEmpty(aramakelimesi))
            {
                sorgu = sorgu.Where(k => k.Ad.ToLower().Contains(aramakelimesi.ToLower()));
            }

           
            if (kategoriId.HasValue && kategoriId > 0)
            {
                sorgu = sorgu.Where(k => k.KategoriId == kategoriId);
            }

            return View(await sorgu.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.YazarlarListesi = new SelectList(_context.YAZARLAR.ToList(), "Id", "AdSoyad");
            ViewBag.KategorilerListesi = new SelectList(_context.Kategoriler.ToList(), "Id", "Ad"); 
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KITAPLAR kitap)
        {
            if (ModelState.IsValid)
            {
                _context.KITAPLAR.Add(kitap);
                await _context.SaveChangesAsync();
                TempData["BasariMesaji"] = "Yeni kitap başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.YazarlarListesi = new SelectList(_context.YAZARLAR.ToList(), "Id", "AdSoyad", kitap.YazarId);
            ViewBag.KategorilerListesi = new SelectList(_context.Kategoriler.ToList(), "Id", "Ad", kitap.KategoriId); 
            return View(kitap);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var kitap = await _context.KITAPLAR.FindAsync(id);
            if (kitap == null) return NotFound();

            ViewBag.YazarlarListesi = new SelectList(_context.YAZARLAR.ToList(), "Id", "AdSoyad", kitap.YazarId);
            ViewBag.KategorilerListesi = new SelectList(_context.Kategoriler.ToList(), "Id", "Ad", kitap.KategoriId); 
            return View(kitap);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KITAPLAR kitap)
        {
            if (id != kitap.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kitap);
                    await _context.SaveChangesAsync();
                    TempData["BasariMesaji"] = "Kitap başarıyla güncellendi.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.YazarlarListesi = new SelectList(_context.YAZARLAR.ToList(), "Id", "AdSoyad", kitap.YazarId);
            ViewBag.KategorilerListesi = new SelectList(_context.Kategoriler.ToList(), "Id", "Ad", kitap.KategoriId); 
            return View(kitap);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var kitap = await _context.KITAPLAR
                .Include(k => k.Yazar)
                .Include(k => k.Kategori) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (kitap == null) return NotFound();

            return View(kitap);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var kitap = await _context.KITAPLAR
                .Include(k => k.Yazar) 
                .Include(k => k.Kategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kitap == null) return NotFound();

            return View(kitap);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kitap = await _context.KITAPLAR.FindAsync(id);
            if (kitap != null)
            {
                _context.KITAPLAR.Remove(kitap);
                await _context.SaveChangesAsync();
                TempData["BasariMesaji"] = "Kitap başarıyla silindi.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}