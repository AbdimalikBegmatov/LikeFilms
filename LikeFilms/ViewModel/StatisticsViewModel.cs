using LikeFilms.Models;

namespace LikeFilms.ViewModel
{
    public class StatisticsViewModel
    {
            public int TotalCount { get; set; }
            public Dictionary<Genre, int> GenreCount { get; set; }
    }
}
