using Championship.Application.ViewModels;
using Championship.Domain.AggregatesModel;
using Championship.Domain.SeedWork;
using System.Threading.Tasks;

namespace Championship.Application.Services
{
    public interface IStandingService
    {
        Task<StandingViewModel> CreateAsync(Tournament tournament);
        Task<Response<StandingViewModel>> GetByIdAsync(string id);
    }
}
