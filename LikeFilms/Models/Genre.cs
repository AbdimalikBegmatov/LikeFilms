using System.ComponentModel.DataAnnotations;

namespace LikeFilms.Models
{
    public enum Genre
    {
        Drama,
        Comedy,
        Romance,
        [Display(Name = "Romantic Comedy")]
        RomCom,
        Crime,
        Mystery,
        Action,
        Adventure

    }
}
