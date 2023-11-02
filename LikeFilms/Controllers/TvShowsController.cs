using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LikeFilms.Data;
using LikeFilms.Models;
using LikeFilms.ViewModel;

namespace LikeFilms.Controllers
{
    public class TvShowsController : Controller
    {
        private readonly ApplicationContext _context;

        public TvShowsController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id, string sortvalue, string byGenre)
        {
            ViewBag.SortValue = sortvalue;
            ViewBag.ByGenre = byGenre;
            int pageSize = 3;
            int totalCount;
            id = id < 1 ? 1 : id;
            IQueryable<TvShow> result;
            if (byGenre is null)
            {
                totalCount = (int)Math.Ceiling(_context.TvShow.Count() / (double)pageSize);
                result = sortvalue switch
                {
                    "TitleSort" => _context.TvShow.OrderBy(t => t.Title).Skip((id - 1) * pageSize).Take(pageSize),
                    "RatingSort" => _context.TvShow.OrderByDescending(t => t.Rating).Skip((id - 1) * pageSize).Take(pageSize),
                    _ => _context.TvShow.OrderBy(t => t.Id).Skip((id - 1) * pageSize).Take(pageSize),
                };
            }
            else
            {
                int genreValue = (int)Enum.Parse(typeof(Genre), byGenre, true);
                totalCount = (int)Math.Ceiling(_context.TvShow.Where(t => ((int)t.Genre) == genreValue).Count() / (double)pageSize);
                result = _context.TvShow.Where(t => ((int)t.Genre) == genreValue).Skip((id - 1) * pageSize).Take(pageSize);
            }

            return View(new DataViewModel<TvShow>()
            {
                CurrentPage = id,
                TotalPages = totalCount,
                Data = result
            });
        }
        [HttpPost]
        public IActionResult Index(string search)
        {
            //int pageSize = 3;
            //int totalCount = (int)Math.Ceiling(_context.TvShow.Where(t=>t.Title.Contains(search)).Count() / (double)pageSize);
            //if (totalCount > pageSize)
            //{
            //    id = 1;
            //}
            if (search!=null)
            {
                var result = _context.TvShow.Where(t => t.Title.ToLower().Contains(search.ToLower()));
                return View(new DataViewModel<TvShow>()
                {
                    Data = result
                });
            }
            
            return RedirectToAction(nameof(Index));
        }

        // GET: TvShows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TvShow == null)
            {
                return NotFound();
            }

            var result= await _context.TvShow.Include(t=>t.Comments).FirstOrDefaultAsync(m => m.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(new TvShowViewModel()
            {
                Id = result.Id,
                Title = result.Title,
                Genre = result.Genre,
                Rating = result.Rating,
                ImageUrl = result.ImageUrl,
                ImdbUrl = result.ImdbUrl,
                Comments = result.Comments,
            });
        }

        // GET: TvShows/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TvShows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Genre,Rating,ImdbUrl,ImageUrl")] TvShow tvShow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tvShow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tvShow);
        }

        // GET: TvShows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TvShow == null)
            {
                return NotFound();
            }

            var tvShow = await _context.TvShow.FindAsync(id);
            if (tvShow == null)
            {
                return NotFound();
            }
            return View(tvShow);
        }

        // POST: TvShows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,Rating,ImdbUrl,ImageUrl")] TvShow tvShow)
        {
            if (id != tvShow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tvShow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TvShowExists(tvShow.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tvShow);
        }

        // GET: TvShows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TvShow == null)
            {
                return NotFound();
            }

            var tvShow = await _context.TvShow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tvShow == null)
            {
                return NotFound();
            }

            return View(tvShow);
        }

        // POST: TvShows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TvShow == null)
            {
                return Problem("Entity set 'ApplicationContext.TvShow'  is null.");
            }
            var tvShow = await _context.TvShow.FindAsync(id);
            if (tvShow != null)
            {
                _context.TvShow.Remove(tvShow);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Card()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Card(CreditCard creditCard)
        {
            if (!ModelState.IsValid)
            {
                return View(creditCard);
            }
            return View(creditCard);
        }
        private bool TvShowExists(int id)
        {
          return (_context.TvShow?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
