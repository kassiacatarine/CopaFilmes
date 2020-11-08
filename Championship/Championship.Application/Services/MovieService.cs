using Championship.Application.ViewModels;
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

        public async Task<IEnumerable<MovieViewModel>> GetMoviesAsync()
        {
            var response = await _httpClient
                .GetAsync($"filmes", HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<MovieViewModel>>(jsonString);
        }
    }
}
