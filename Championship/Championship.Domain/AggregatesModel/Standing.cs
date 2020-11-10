using Championship.Domain.SeedWork;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Championship.Domain.AggregatesModel
{
    public class Standing : Entity
    {
        public Tournament Tournament { get; private set; }
        public IEnumerable<Ranking> Rankings { get; private set; }

        public Standing(Tournament tournament)
        {
            Tournament = tournament;
            CreateRanking();
        }

        private void CreateRanking()
        {
            Rankings = Tournament.Movies
                .OrderBy(m => m.Titulo)
                .Select(m => new Ranking(m)).ToList();
        }

        public async Task RunMatchesAsync()
        {
            var movies = Tournament.Movies
                .OrderBy(m => m.Titulo);
            var firstHalf = movies.Take(4)
                .ToList();
            var secondHalf = movies
                .Skip(4)
                .Take(4)
                .Reverse()
                .ToList();

            await CalculateRankingsAsync(firstHalf, secondHalf);
            Rankings = Rankings.OrderBy(r => r.Score);
        }

        public async Task<bool> CalculateRankingsAsync(List<Movie> firstHalf, List<Movie> secondHalf)
        {
            if (firstHalf.Count <= 0 || secondHalf.Count <= 0)
                return true;
            var newFirstHalf = new List<Movie>();
            var newSecondHalf = new List<Movie>();
            for (int i = 0; i < firstHalf.Count; i++)
            {
                var winner = GenerateMatch(firstHalf[i], secondHalf[i]);
                if (i % 2 == 0)
                    newFirstHalf.Add(winner);
                else
                    newSecondHalf.Add(winner);
            }
            return await CalculateRankingsAsync(newFirstHalf, newSecondHalf);
        }

        public Movie GenerateMatch(Movie first, Movie second)
        {
            var movie = GetWinnerMatch(first, second);
            var ranking = Rankings.FirstOrDefault(r => r.Movie.Id == movie.Id);
            ranking.AddScoreWinner();
            return movie;
        }

        public Movie GetWinnerMatch(Movie first, Movie second)
        {
            if (first.Nota != second.Nota)
                return first.Nota > second.Nota ? first : second;

            return first.Titulo.CompareTo(second.Titulo) <= 0 ? first : second;
        }
    }
}
