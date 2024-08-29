using Checkpoint2.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace Checkpoint2.Models.Entities
{
    public class BuyedArticle
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser Payer { get; set; }

        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
