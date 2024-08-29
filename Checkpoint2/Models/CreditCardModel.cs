using System.ComponentModel.DataAnnotations;

namespace Checkpoint2.Models
{
    public class CreditCardModel
    {
        [Required(ErrorMessage = "Le nom de la carte est obligatoire.")]
        [Display(Name = "Nom de la carte")]
        [StringLength(50, ErrorMessage = "Le nom ne peut pas faire plus de 50 caractères.")]
        public string CardName { get; set; }

        [Required(ErrorMessage = "Le numéro de la carte est obligatoire.")]
        //[CreditCard]
        [Display(Name = "Numéro de la carte")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Le mois d'expiration de la carte est obligatoire.")]
        [Display(Name = "Mois d'expiration")]
        [Range(1, 12, ErrorMessage = "Veuillez entrer un mois valide.")]
        public int ExpirationMonth { get; set; }

        [Required(ErrorMessage = "L'année d'expiration de la carte est obligatoire.")]
        [Display(Name = "Année d'expiration")]
        [Range(2024, 2044, ErrorMessage = "Veuillez entrer une année valide.")]
        public int ExpirationYear { get; set; }

        [Required(ErrorMessage = "Le CVV est obligatoire.")]
        [Display(Name = "CVV")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "Le cryptogramme de sécurité ne peut contenir que 3 ou 4 chiffres.")]
        public string CVV { get; set; }
    }
}
