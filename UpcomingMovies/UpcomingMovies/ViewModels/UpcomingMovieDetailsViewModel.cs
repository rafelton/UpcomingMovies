using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpcomingMovies.Models;
using UpcomingMovies.Views;
using Xamarin.Forms;

namespace UpcomingMovies.ViewModels
{
    public class UpcomingMovieDetailsViewModel : BaseNotify
    {
        public UpcomingMovieHolder Movie { get; set; }
        private Page _page;
        public UpcomingMovieDetailsViewModel(Page page, UpcomingMovieHolder movie)
        {
            this._page = page;
            this.Movie = movie;
        }
        public Command ViewPoster
        {
            get
            {
                return new Command(async () =>
                {
                    await _page.Navigation.PushAsync(new MoviePosterPage(Movie.PosterPath));
                });
            }
        }
    }
}
