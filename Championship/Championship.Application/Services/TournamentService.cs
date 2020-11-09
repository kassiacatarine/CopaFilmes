using AutoMapper;
using Championship.Application.ViewModels;
using Championship.Domain.AggregatesModel;
using Championship.Domain.SeedWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Championship.Application.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public TournamentService(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }
        public async Task<Response<TournamentViewModel>> CreateAsync(CreateTournamentViewModel model)
        {
            var moviesResponse = await _movieService.GetMoviesAsync();

            var moviesSelected = moviesResponse.Data.Where(m => model.MoviesIds.Contains(m.Id));

            var tournament = new Tournament(_mapper.Map<IEnumerable<Movie>>(moviesSelected));
            return new Response<TournamentViewModel>(true, "Successfully created tournament");
        }
    }
}
