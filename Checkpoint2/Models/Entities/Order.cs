using Checkpoint2.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace Checkpoint2.Models.Entities
{
    public class Order
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser Payer { get; set; }
        public List<CartArticle> Articles { get; set; } = new List<CartArticle>();

        [Required]
        public DateTime Date { get; set; }
    }
}
