using Championship.Domain.SeedWork;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Championship.Domain.AggregatesModel
{
    public class Ranking : Entity
    {
        public int Score { get; private set; }
        public Movie Movie { get; private set; }

        public Ranking(Movie movie)
        {
            Score = 0;
            Movie = movie;
        }

        public void AddScoreWinner()
        {
            Score++;
        }
    }
}
