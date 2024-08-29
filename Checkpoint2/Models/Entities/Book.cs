using System.ComponentModel.DataAnnotations;

namespace Checkpoint2.Models.Entities
{
    public class Book
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
        public int? AuthorId { get; set; } = null;
        public Author? Author { get; set; }
        public int? CategoryId { get; set; } = null;
        public Category? Category { get; set; }
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CoverImage { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
    }
}
