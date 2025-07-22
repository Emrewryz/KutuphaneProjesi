using KutuphaneProjesi.Data; 
using KutuphaneProjesi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using System.Diagnostics;

namespace KutuphaneProjesi.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                
                var yeniKitaplar = await _context.KITAPLAR
                    .Include(k => k.Yazar)
                    .OrderByDescending(k => k.Id)
                    .Take(3)
                    .ToListAsync();

                return View(yeniKitaplar);
            }

            return View();
        }

    }
}