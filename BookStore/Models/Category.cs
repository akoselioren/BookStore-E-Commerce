using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Kategori adı alanı boş geçilemez.")]
        [StringLength(45, MinimumLength = 2, ErrorMessage = "Kategori alanı min:2 - max: 45 karakter olabilir.")]
        public string CategoryName { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
