using System.ComponentModel.DataAnnotations;
namespace KutuphaneProjesi.Models
{
    public class UYELER
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ad Soyad alanı boş bırakılamaz.")]
        [StringLength(100, ErrorMessage = "Ad Soyad en fazla 100 karakter olabilir.")]
        [Display(Name = "Üye Ad Soyad")]

        public string AdSoyad { get; set; } = string.Empty;
        [Required(ErrorMessage = "E-posta alanı boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [StringLength(100, ErrorMessage = "E-posta en fazla 100 karakter olabilir.")]
        [Display(Name = "E-posta Adresi")]

        public string Eposta { get; set; } = string.Empty;

        public string? Telefon { get; set; }

    }
}
