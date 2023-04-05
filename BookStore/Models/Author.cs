using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AuthorName { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
