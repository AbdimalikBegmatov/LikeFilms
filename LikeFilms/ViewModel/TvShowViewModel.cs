using LikeFilms.Models;
using System.ComponentModel.DataAnnotations;

namespace LikeFilms.ViewModel
{
    public class TvShowViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public Genre Genre { get; set; }
        public decimal Rating { get; set; }
        public string ImdbUrl { get; set; }
        public string? ImageUrl { get; set; }
        public List<Comment>? Comments { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public int TvShowId { get; set; }
    }
}
