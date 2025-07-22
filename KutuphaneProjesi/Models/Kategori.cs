using System.ComponentModel.DataAnnotations;

namespace KutuphaneProjesi.Models
{
    public class Kategori
    {
        public int Id { get; set; }
        [Required]
        public string Ad { get; set; } = string.Empty;
    }
}