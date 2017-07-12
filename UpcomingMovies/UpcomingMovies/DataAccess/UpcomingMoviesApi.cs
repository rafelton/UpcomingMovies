using ModernHttpClient;
using Plugin.Connectivity;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UpcomingMovies.Models;

namespace UpcomingMovies.DataAccess
{

    [Headers("Accept: application/json")]
    public interface IUpcomingMoviesApi
    {
        [Get("/movie/upcoming?api_key={apiKey}&language={language}&page={page}")]
        Task<UpcomingMoviesDto> GetUpcomingMovies(string apiKey, string language, int page);

        [Get("/genre/movie/list?api_key={apiKey}&language={language}")]
        Task<GenresDto> GetGenresList(string apiKey, string language);

        [Get("/search/movie?api_key={apiKey}&language={language}&page={page}&query={query}")]
        Task<SearchMoviesDto> SearchMovies(string apiKey, string language, int page, string query);
    }

    public class Services
    {
        IUpcomingMoviesApi Current;
        public Services()
        {
            var client = new HttpClient(new NativeMessageHandler())
            {
                BaseAddress = new Uri(Constants.THEMOVIEDB_API_URL)
            };
            Current = RestService.For<IUpcomingMoviesApi>(client);
        }
        public async Task<UpcomingMoviesDto> GetUpcomingMovies(int page)
        {
            CheckInternetConnection();
            return await Current.GetUpcomingMovies(Constants.API_KEY, Constants.APP_LANGUAGE, page);
        }

        public async Task<GenresDto> GetGenresList()
        {
            CheckInternetConnection();
            return await Current.GetGenresList(Constants.API_KEY, Constants.APP_LANGUAGE);
        }
        public async Task<SearchMoviesDto> SearchMovies(int page, string query)
        {
            CheckInternetConnection();
            return await Current.SearchMovies(Constants.API_KEY, Constants.APP_LANGUAGE, page, query);
        }
        private void CheckInternetConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
                throw new Exception("You are offline");
        }
    }
}
