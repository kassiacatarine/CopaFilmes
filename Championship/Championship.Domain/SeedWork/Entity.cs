using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Championship.Domain.SeedWork
{
    public abstract class Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; protected set; }
    }
}
