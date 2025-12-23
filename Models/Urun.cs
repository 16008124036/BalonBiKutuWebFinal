using System.ComponentModel.DataAnnotations;

namespace HediyelikEsya.Models
{
    public class Urun
    {
        [Key]
        public int Id { get; set; } // Otomatik artan numara [cite: 79]

        [Required(ErrorMessage = "Ürün adı boş bırakılamaz.")]
        [Display(Name = "Ürün Adı")]
        public string Ad { get; set; } = string.Empty;

        [Required(ErrorMessage = "Fiyat girmelisiniz.")]
        [Display(Name = "Fiyat (TL)")]
        public decimal Fiyat { get; set; }

        [Display(Name = "Ürün Açıklaması")]
        public string? Aciklama { get; set; }

        [Display(Name = "Resim Linki")]
        public string? ResimUrl { get; set; } // Ürün resmi için
    }
}