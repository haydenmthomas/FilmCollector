using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmCollector.Models
{
    public static class TempStorage
    {
        private static List<FilmSubmission> films = new List<FilmSubmission>();

        public static IEnumerable<FilmSubmission> Films => films;

        public static void AddFilm(FilmSubmission film)
        {
            if (film.Title.ToLower() == "independence day")
            {

            }
            else
            {
                films.Add(film);
            }
        }
    }
}
