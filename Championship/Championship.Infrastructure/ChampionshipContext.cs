using Championship.Domain.AggregatesModel;
using Championship.Domain.SeedWork;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Championship.Infrastructure
{
    public class ChampionshipContext : IChampionshipContext
    {
        private readonly IMongoDatabase _db;
        private Dictionary<Type, string> DocumentNames { get; } = new Dictionary<Type, string>();
        public ChampionshipContext(IOptions<DbSettings> settings, IMongoClient client)
        {
            _db = client.GetDatabase(settings.Value.Database);
            DocumentNames.Add(typeof(Movie), $"movies");
            DocumentNames.Add(typeof(Ranking), $"rankings");
            DocumentNames.Add(typeof(Tournament), $"tournaments");
            DocumentNames.Add(typeof(Standing), $"standings");
        }

        public IMongoCollection<Movie> Movies => _db.GetCollection<Movie>(CollectionName<Movie>());
        public IMongoCollection<Ranking> Rankings => _db.GetCollection<Ranking>(CollectionName<Ranking>());
        public IMongoCollection<Tournament> Tournaments => _db.GetCollection<Tournament>(CollectionName<Tournament>());
        public IMongoCollection<Standing> Standings => _db.GetCollection<Standing>(CollectionName<Standing>());

        private string CollectionName<T>() { return DocumentNames[typeof(T)]; }
        public IMongoCollection<T> GetCollection<T>() where T : Entity
        {
            return _db.GetCollection<T>(CollectionName<T>());
        }
    }
}
