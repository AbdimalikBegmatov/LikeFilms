
using LikeFilms.Data;
using LikeFilms.Models;
using LikeFilms.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LikeFilms.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationContext _context;

        public CommentController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddComment(TvShowViewModel tvShowView)
        {
            
            if (!ModelState.IsValid)
            {
                return View("~/Views/TvShows/Details.cshtml", tvShowView);
            }


            Comment comment = new Comment();

            comment.TvShowId = tvShowView.Id;
            comment.Name = tvShowView.Name;
            comment.Text = tvShowView.Text;

            _context.Comment.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("Details","TvShows", new {id= comment.TvShowId});
        }
    }
}
