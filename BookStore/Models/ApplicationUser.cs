using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required(ErrorMessage ="Ad Alanı boş geçilemez.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Ad alanı min:2 - max: 30 karakter olabilir.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyad Alanı boş geçilemez.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Soyad alanı min:2 - max: 30 karakter olabilir.")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Adres Alanı boş geçilemez.")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "Şehir Alanı boş geçilemez.")]
        public string City { get; set; }
        [Required(ErrorMessage = "İlçe Alanı boş geçilemez.")]
        public string District { get; set; }
        [Required(ErrorMessage = "Posta Kodu alanı boş geçilemez.")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Geçersiz Posta Kodu !")]
        public string PostCode { get; set; }
        [NotMapped]
        public string Role { get; set; }
    }
}
