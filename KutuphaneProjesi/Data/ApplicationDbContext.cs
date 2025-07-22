using KutuphaneProjesi.Models;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneProjesi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<KITAPLAR> KITAPLAR { get; set; }
        public DbSet<YAZARLAR> YAZARLAR { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
    }
}
