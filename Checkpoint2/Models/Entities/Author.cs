using System.ComponentModel.DataAnnotations;

namespace Checkpoint2.Models.Entities
{
    public class Author
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
    }
}
