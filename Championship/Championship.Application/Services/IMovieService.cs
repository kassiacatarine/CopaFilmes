using Championship.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Championship.Application.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieViewModel>> GetMoviesAsync();
    }
}
