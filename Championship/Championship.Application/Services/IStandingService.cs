using Championship.Domain.AggregatesModel;
using System.Threading.Tasks;

namespace Championship.Application.Services
{
    public interface IStandingService
    {
        Task<bool> CreateAsync(Tournament tournament);
    }
}
