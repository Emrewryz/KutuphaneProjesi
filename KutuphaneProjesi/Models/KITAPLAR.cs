using System.ComponentModel.DataAnnotations;
namespace KutuphaneProjesi.Models
{
    public class KITAPLAR
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kitap adı alanı boş bırakılamaz.")]
        [StringLength(200, ErrorMessage = "Kitap adı en fazla 200 karakter olabilir.")]
        [Display(Name = "Kitap Adı")]
        public string Ad { get; set; } = string.Empty;

        [Display(Name = "Sayfa Sayısı")]
        [Range(1, 10000, ErrorMessage = "Sayfa sayısı 1 ile 10000 arasında olmalıdır.")]
        public int? SayfaSayisi { get; set; }
        [Display(Name = "Açıklama")]
        public string? Aciklama { get; set; }

        [Display(Name = "Görsel URL")]
        public string? GorselUrl { get; set; }
        [Display(Name = "İçerik")]
        public string? Icerik { get; set; }

        [Required(ErrorMessage = "Lütfen bir yazar seçin.")]
        [Display(Name = "Yazar")]
        public int YazarId { get; set; }

        public YAZARLAR? Yazar { get; set; }
        [Display(Name = "Kategori")]
        public int? KategoriId { get; set; } 
        public Kategori? Kategori { get; set; }
    }
}