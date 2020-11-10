using Championship.Domain.SeedWork;
using System.Threading.Tasks;

namespace Championship.Infrastructure.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> GetByIdAsync(string id);
        Task InsertOneAsync(T entity);
    }
}
