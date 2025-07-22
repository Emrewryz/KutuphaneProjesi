using System.ComponentModel.DataAnnotations;
namespace KutuphaneProjesi.Models
{
    public class YAZARLAR
    {
        public int Id { get; set; }



        [Required(ErrorMessage = "Yazar adı alanı boş bırakılamaz.")]

        [StringLength(100, ErrorMessage = "Yazar adı en fazla 100 karakter olabilir.")]

        [Display(Name = "Yazar Adı Soyadı")]
        public string AdSoyad { get; set; } = string.Empty;
    }
}
