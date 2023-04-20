using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Yazar Adı Soyadı alanı boş geçilemez.")]
        [StringLength(45, MinimumLength = 2, ErrorMessage = "Ad Soyad alanı min:2 - max: 45 karakter olabilir.")]
        public string AuthorName { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
