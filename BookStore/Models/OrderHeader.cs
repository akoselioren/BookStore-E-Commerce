using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace BookStore.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime OrderDate { get; set; }
        public double OrderTotal { get; set; }
        public string OrderStatus { get; set; }
        [Required(ErrorMessage = "Kullanıcı adı alanı boş geçilemez.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Ad alanı min:2 - max: 30 karakter olabilir.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Kullanıcı soyadı alanı boş geçilemez.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Soyad alanı min:2 - max: 30 karakter olabilir.")]
        public string SurName { get; set; }
        [Required(ErrorMessage = "Telefon numarası alanı boş geçilemez.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Geçersiz telefon numarası !")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Adres alanı boş geçilemez.")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "Şehir alanı boş geçilemez.")]
        public string City { get; set; }
        [Required(ErrorMessage = "İlçe alanı boş geçilemez.")]
        public string District { get; set; }
        [Required(ErrorMessage = "Posta Kodu alanı boş geçilemez.")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Geçersiz Posta Kodu !")]
        public string PostCode { get; set; }
        [Required(ErrorMessage = "Kart sahibinin adı alanı boş geçilemez.")]
        public string CartName { get; set; }
        [Required(ErrorMessage = "Kart numarası alanı boş geçilemez.")]
        public string CartNumber { get; set; }
        [Required(ErrorMessage = "Ay alanı boş geçilemez.")]
        [Range(1,12,ErrorMessage = "1 ile 12 arasında değer giriniz.")]
        public string ExpirationMonth { get; set; }
        [Required(ErrorMessage = "Yıl alanı boş geçilemez.")]
        [Range(22, 99, ErrorMessage = "2 haneli ve 22'den büyük değer giriniz.")]
        public string ExpirationYear { get; set; }
        [Required(ErrorMessage = "Cvc alanı boş geçilemez.")]
        [Range(100, 999, ErrorMessage = "3 haneli değer giriniz.")]
        public string Cvc { get; set; }
    }
}
