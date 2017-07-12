using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpcomingMovies.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UpcomingMovies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpcomingMoviesListPage : ContentPage
    {
        public UpcomingMoviesListPage()
        {
            InitializeComponent();
            BindingContext = new UpcomingMoviesListViewModel(this);
        }
    }
}