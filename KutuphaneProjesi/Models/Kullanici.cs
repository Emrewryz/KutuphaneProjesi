using System.ComponentModel.DataAnnotations;
namespace KutuphaneProjesi.Models
{
    public class Kullanici
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Kullanıcı adı alanı boş bırakılamaz.")]
        [StringLength(50, ErrorMessage = "Kullanıcı adı en fazla 50 karakter olabilir.")]
        [Display(Name = "Kullanıcı Adı")]
        public string KullaniciAdi { get; set; } = string.Empty;
        [Required(ErrorMessage = "E-posta alanı boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [StringLength(100, ErrorMessage = "E-posta en fazla 100 karakter olabilir.")]
        public string Eposta { get; set; } = string.Empty;
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        [StringLength(100, ErrorMessage = "Şifre en az 6 ve en fazla 100 karakter olabilir.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Sifre { get; set; } = string.Empty;
        public string Rol { get; set; } = "Uye";

    }
}
