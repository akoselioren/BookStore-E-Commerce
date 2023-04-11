using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Count= 1;
        }
        [Key]
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }
        public int BookId { get; set; }
        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }
}
