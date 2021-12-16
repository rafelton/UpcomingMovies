using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpcomingMovies.Models
{
    public class UpcomingMovieHolder
    {
        public string Name { get; set; }
        public string BackdropPath { get; set; }
        public string PosterPath { get; set; }
        public string Genre { get; set; }
        public string Overview { get; set; }
        public string ReleaseDate { get; set; }
    }
}
