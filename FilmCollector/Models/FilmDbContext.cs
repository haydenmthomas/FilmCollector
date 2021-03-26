using System;
using Microsoft.EntityFrameworkCore;

namespace FilmCollector.Models
{
    //Allows for us to use the database and the model together

    public class FilmDbContext : DbContext
    {
        public FilmDbContext (DbContextOptions<FilmDbContext> options) : base(options)
        {

        }

        public DbSet<FilmSubmission> Films { get; set; }
    }
}
