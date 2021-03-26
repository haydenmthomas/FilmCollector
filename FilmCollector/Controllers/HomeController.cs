using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FilmCollector.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmCollector.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private FilmDbContext _context { get; set; }

        public HomeController(ILogger<HomeController> logger, FilmDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        //Gets the home page
        public IActionResult Index()
        {
            return View();
        }

        //Gets the podcast page
        public IActionResult Podcasts()
        {
            return View();
        }

        //Gets the submit film view
        [HttpGet]
        public IActionResult SubmitFilm()
        {
            return View();
        }

        //Allows us to add a film to the database
        [HttpPost]
        public IActionResult SubmitFilm(FilmSubmission filmSubmission)
        {

            if (ModelState.IsValid)
            {
                _context.Films.Add(filmSubmission);
                _context.SaveChanges();
                return View("Confirmation", filmSubmission);
            }
            else
            {
                return View(filmSubmission);
            }
        }

        //Outputs the list of films using the database 
        public IActionResult Films()
        {
            return View(_context.Films);
        }

        //Allows us to delete a film from the list
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = _context.Films
                .Where(x => x.FilmID == id)
                .FirstOrDefault();
            _context.Films.Remove(movie);
            _context.SaveChanges();
            return RedirectToAction(nameof(Films));
        }

        //Sets a variable to use inside of other controllers
        public static int fID;

        //Allows us to edit a film by passing its info to the edit film page
        [HttpPost]
        public IActionResult Edit(int id)
        {
            fID = id;
            return View("EditFilm", new FilmViewModel
            {
                filmsMod = _context.Films.Single(x => x.FilmID == fID),
                Id = fID
            });
        }

        //Takes the edit of the film and makes the changes then outputs to the film list
        [HttpPost]
        public IActionResult Update(FilmViewModel fMod)
        {
            if (ModelState.IsValid)
            {
                var film = _context.Films
                .Where(x => x.FilmID == fID)
                .FirstOrDefault();

                _context.Entry(film).Property(x => x.Category).CurrentValue = fMod.filmsMod.Category;
                _context.Entry(film).Property(x => x.Title).CurrentValue = fMod.filmsMod.Title;
                _context.Entry(film).Property(x => x.Year).CurrentValue = fMod.filmsMod.Year;
                _context.Entry(film).Property(x => x.Director).CurrentValue = fMod.filmsMod.Director;
                _context.Entry(film).Property(x => x.Rating).CurrentValue = fMod.filmsMod.Rating;
                _context.Entry(film).Property(x => x.Edited).CurrentValue = fMod.filmsMod.Edited;
                _context.Entry(film).Property(x => x.LentTo).CurrentValue = fMod.filmsMod.LentTo;
                _context.Entry(film).Property(x => x.Notes).CurrentValue = fMod.filmsMod.Notes;
                _context.SaveChanges();

                return RedirectToAction("Films");

            }
            else
            {
                return View("EditFilm", new FilmViewModel
                {
                    filmsMod = _context.Films.Single(x => x.FilmID == fID),
                    Id = fID
                });
            }

        }

        //Outputs the privacy page
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
