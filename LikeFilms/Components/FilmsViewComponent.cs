using LikeFilms.Data;
using LikeFilms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LikeFilms.Components
{
    public class FilmsViewComponent:ViewComponent 
    {
        private readonly ApplicationContext _context;

        public FilmsViewComponent(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int filmId,string byGenre)
        {
            int genreValue = (int)Enum.Parse(typeof(Genre), byGenre, true);
            var result = await _context.TvShow.Where(t=>(int)t.Genre == genreValue && t.Id!=filmId)
                .Take(5).OrderByDescending(t=>t.Rating).ToListAsync();
            return View(result);
        }
    }
}
