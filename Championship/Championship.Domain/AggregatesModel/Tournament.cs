using Championship.Domain.SeedWork;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Championship.Domain.AggregatesModel
{
    public class Tournament : Entity
    {
        public IEnumerable<Movie> Movies { get; set; }
        public Tournament(IEnumerable<Movie> movies)
        {
            Movies = movies;
        }
    }
}
