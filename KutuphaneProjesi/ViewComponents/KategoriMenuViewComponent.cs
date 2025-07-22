using KutuphaneProjesi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneProjesi.ViewComponents
{
    public class KategoriMenuViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public KategoriMenuViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var kategoriler = await _context.Kategoriler.ToListAsync();
            return View(kategoriler);
        }
    }
}