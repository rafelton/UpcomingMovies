using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpcomingMovies.DataAccess;
using UpcomingMovies.Models;
using UpcomingMovies.Views;
using Xamarin.Forms;

namespace UpcomingMovies.ViewModels
{
    public class UpcomingMoviesListViewModel : BaseNotify
    {
        private ListView _resultsListView;
        private SearchBar _searchBar;
        private Page _page;
        public int CurrentPage { get; set; }

        public GenresDto GenresList { get; set; }
        private ObservableCollection<UpcomingMovieHolder> _results = new ObservableCollection<UpcomingMovieHolder>();
        public ObservableCollection<UpcomingMovieHolder> Results
        {
            get
            {
                return _results;
            }
            set
            {
                _results = value;
                OnPropertyChanged("Results");
            }
        }

        private bool _busy;
        public bool Busy
        {
            get
            {
                return _busy;
            }
            set
            {
                _busy = value;
                OnPropertyChanged("Busy");
            }
        }

        private Services _api;
        public UpcomingMoviesListViewModel(Page page)
        {
            this._page = page;
            _api = new Services();
            _resultsListView = page.FindByName<ListView>("resultsListView");
            _searchBar = page.FindByName<SearchBar>("searchBar");
            _resultsListView.ItemTapped += (sender, e) =>
            {
                _page.Navigation.PushAsync(new UpcomingMovieDetailsPage((UpcomingMovieHolder)e.Item));
            };
            _resultsListView.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
            _resultsListView.ItemAppearing += async (sender, e) =>
            {
                var items = ((ListView)sender).ItemsSource as IList;
                if (items != null && e.Item == items[Math.Max(items.Count - 4, 0)])
                    if (IsSearchMode)
                        await SearchMovies(CurrentPage + 1);
                    else
                        await GetUpcomingData(CurrentPage + 1);

            };
            _searchBar.TextChanged += async (sender, e) =>
            {
                if (IsSearchMode)
                    await SearchMovies(1);
                else
                    await GetUpcomingData(1);

            };
            Device.BeginInvokeOnMainThread(async () =>
                await GetUpcomingData(1));
        }

        public bool IsSearchMode
        {
            get
            {
                return !String.IsNullOrEmpty(_searchBar.Text);
            }
        }
        public Command Refresh
        {
            get
            {
                return new Command(async () =>
                {
                    Results.Clear();
                    await GetResultsAndFillList(1);
                    _resultsListView.IsRefreshing = false;
                }, () => !Busy);
            }
        }
        public Command Search
        {
            get
            {
                return new Command(async () =>
                {
                    if (!String.IsNullOrEmpty(_searchBar.Text))
                        await SearchMovies(1);
                }, () => !Busy);
            }
        }

        async Task GetUpcomingData(int page)
        {
            try
            {
                Busy = true;
                if (page == 1)
                {
                    GenresList = await _api.GetGenresList();
                    Results.Clear();
                }

                await GetResultsAndFillList(page);
            }
            catch (Exception ex)
            {
                await _page.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                Busy = false;
            }
        }
        private async Task GetResultsAndFillList(int page)
        {
            var resultList = await _api.GetUpcomingMovies(page);
            CurrentPage = resultList.page;
            FillListView(resultList.results);
        }
        private void FillListView(Result[] results)
        {
            foreach (var movie in results)
            {
                Results.Add(new UpcomingMovieHolder()
                {
                    Name = movie.title,
                    BackdropPath = Constants.IMAGE_API_URL + movie.backdrop_path,
                    PosterPath = Constants.IMAGE_API_URL +  movie.poster_path,
                    ReleaseDate = movie.release_date,
                    Overview = movie.overview,
                    Genre = String.Join(", ", GenresList.genres.Where(g => movie.genre_ids.Contains(g.id)).Select(g => g.name))
                });
            }
        }
        async Task SearchMovies(int page)
        {
            try
            {
                Busy = true;
                if (page == 1)
                    Results.Clear();
                var searchList = await _api.SearchMovies(page, _searchBar.Text);
                CurrentPage = searchList.page;
                FillListView(searchList.results);
            }
            catch (Exception ex)
            {
                await _page.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                Busy = false;
            }
        }
    }
}
