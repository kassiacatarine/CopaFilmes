using AutoMapper;
using Championship.Application.ViewModels;
using Championship.Domain.AggregatesModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Championship.UnitTests.Builders.Domain
{
    public class TournamentBuilder
    {
        private Tournament _tournament;
        public IEnumerable<Movie> Movies => GetMovies();

        public TournamentBuilder()
        {
            _tournament = WithDefaultValues();
        }

        public Tournament WithDefaultValues()
        {
            _tournament = new Tournament(Movies);
            return _tournament;
        }

        private IEnumerable<Movie> GetMovies()
        {
            string content = File.ReadAllText($"DataTests{Path.DirectorySeparatorChar}Movies.json");
            var data = JsonConvert.DeserializeObject<IEnumerable<Movie>>(content).Take(8);
            return data;
        }

        public Tournament Build()
        {
            return _tournament;
        }
    }
}
