using System.Collections.Generic;

namespace Championship.Domain.AggregatesModel
{
    public class Tournament
    {
        public Tournament(IEnumerable<Movie> movies)
        {
            Movies = movies;
        }

        public string Id { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
    }
}
