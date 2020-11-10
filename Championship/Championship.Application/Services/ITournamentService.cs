using Championship.Application.ViewModels;
using Championship.Domain.SeedWork;
using System.Threading.Tasks;

namespace Championship.Application.Services
{
    public interface ITournamentService
    {
        Task<Response<string>> CreateAsync(CreateTournamentViewModel model);
    }
}
