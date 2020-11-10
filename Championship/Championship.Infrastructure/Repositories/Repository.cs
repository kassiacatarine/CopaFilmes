using Championship.Domain.SeedWork;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Championship.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly IChampionshipContext _context;
        private IMongoCollection<T> Collection => _context.GetCollection<T>();
        public Repository(IChampionshipContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var results = await Collection.FindAsync(t => t.Id == id);
            return results.FirstOrDefault();
        }

        public async Task InsertOneAsync(T entity)
        {
            await Collection.InsertOneAsync(entity);
        }
    }
}
