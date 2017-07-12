using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpcomingMovies.Views;
using Xamarin.Forms;

namespace UpcomingMovies.ViewModels
{
    public class MoviePosterViewModel : BaseNotify
    {
        private string posterPath;
        public string PosterPath
        {
            get => posterPath; set { posterPath = value; OnPropertyChanged("PosterPath"); }
        }
        public MoviePosterViewModel(string posterPath)
        {
            this.PosterPath = posterPath;
        }
    }
}
