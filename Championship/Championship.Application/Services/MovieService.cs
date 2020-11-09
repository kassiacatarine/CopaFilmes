using Championship.Application.ViewModels;
using Championship.Domain.SeedWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Championship.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;

        public MovieService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://copafilmes.azurewebsites.net/api/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient = client;
        }

        public async Task<Response<IEnumerable<MovieViewModel>>> GetMoviesAsync()
        {
            var response = await _httpClient
                .GetAsync($"filmes", HttpCompletionOption.ResponseHeadersRead);

            if (!response.IsSuccessStatusCode)
                return new Response<IEnumerable<MovieViewModel>>(false, "Error connecting to the movie service");

            var jsonString = await response.Content.ReadAsStringAsync();
            return new Response<IEnumerable<MovieViewModel>>(
                true, 
                "Searching for movies successfully", 
                JsonConvert.DeserializeObject<IEnumerable<MovieViewModel>>(jsonString)
            );
        }
    }
}
