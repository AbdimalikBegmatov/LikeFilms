using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LikeFilms.Models;

namespace LikeFilms.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext (DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<LikeFilms.Models.TvShow> TvShow { get; set; } = default!;

        public DbSet<LikeFilms.Models.Comment> Comment { get; set; } = default!;
    }
}
