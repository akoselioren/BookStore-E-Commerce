using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }
        [Required(ErrorMessage = "Kitap Adı alanı boş geçilemez.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Kitap Adı alanı min:2 - max: 100 karakter olabilir.")]
        public string Title { get; set; }
       [Required(ErrorMessage = "Sayfa sayısı alanı boş geçilemez.")]
        public int PageCount { get; set; }
        [Required(ErrorMessage = "Tarih alanı boş geçilemez.")]
        public DateTime PublishDate { get; set; }
        [Required(ErrorMessage = "Açıklama alanı boş geçilemez.")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Açıklama alanı min:2 - max: 500 karakter olabilir.")]
        public string Description { get; set; }
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "Fiyat alanı boş geçilemez.")]
        [Range(1, 6000, ErrorMessage = "1-6000 arasında değer giriniz.")]
        public double Price { get; set; }
        public int StockCount { get; set; } = 100;
        public int SellCount { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public Category Category  { get; set;}
        public Author Author  { get; set;}
    }
}
