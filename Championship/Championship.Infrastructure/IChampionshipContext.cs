using Championship.Domain.AggregatesModel;
using Championship.Domain.SeedWork;
using MongoDB.Driver;

namespace Championship.Infrastructure
{
    public interface IChampionshipContext
    {
        public IMongoCollection<Movie> Movies { get; }
        public IMongoCollection<Ranking> Rankings { get; }
        public IMongoCollection<Tournament> Tournaments { get; }
        public IMongoCollection<Standing> Standings { get; }
        public IMongoCollection<T> GetCollection<T>() where T : Entity;
    }
}
