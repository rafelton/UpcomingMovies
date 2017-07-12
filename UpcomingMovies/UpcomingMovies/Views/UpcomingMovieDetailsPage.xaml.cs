using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpcomingMovies.Models;
using UpcomingMovies.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace UpcomingMovies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpcomingMovieDetailsPage : ContentPage
    {
        public UpcomingMovieDetailsPage(UpcomingMovieHolder movie)
        {
            InitializeComponent();        
            BindingContext = new UpcomingMovieDetailsViewModel(this, movie);
        }
    }
}