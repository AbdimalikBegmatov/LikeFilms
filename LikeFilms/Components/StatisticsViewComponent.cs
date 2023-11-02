using LikeFilms.Data;
using LikeFilms.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LikeFilms.Components
{
    public class StatisticsViewComponent : ViewComponent
    {
        private readonly ApplicationContext _context;

        public StatisticsViewComponent(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var genreCount = await _context.TvShow.
                GroupBy(t => t.Genre)
                    .Select(t => new
                    { 
                        Key = t.Key,
                        Count=t.Count()
                    })
                    .ToDictionaryAsync(k=>k.Key,c=>c.Count);

            return View(new StatisticsViewModel
            {
                TotalCount = genreCount.Values.Sum(),
                GenreCount = genreCount
            });
        }
    }
}
