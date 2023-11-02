using System.ComponentModel.DataAnnotations;

namespace LikeFilms.Models
{
    public class CreditCard
    {
        [Required]
        [CreditCard]
        public string NumberCard { get; set; }
    }
}
