using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmCollector.Models
{
    //Allows for using 2 versions of a submitted film for editing
    public class FilmViewModel
    {
        public int Id;
        public FilmSubmission filmsMod { get; set; }
    }
}

