using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int ReadCount { get; set; }
        public double? Price { get; set; }
        public bool? IsStock { get; set; }
        public bool? IsActive { get; set; } = true;
        public Category Category  { get; set;}
        public Author Author  { get; set;}
    }
}
