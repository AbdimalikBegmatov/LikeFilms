using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace LikeFilms.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [DataType(DataType.Text)]
        public string Text { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public int TvShowId { get; set; }
        public TvShow TvShow { get; set; }
    }
}
