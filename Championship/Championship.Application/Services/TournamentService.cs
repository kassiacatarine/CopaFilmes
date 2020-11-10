using AutoMapper;
using Championship.Application.ViewModels;
using Championship.Domain.AggregatesModel;
using Championship.Domain.SeedWork;
using Championship.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Championship.Application.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly IMovieService _movieService;
        private readonly IStandingService _standingService;
        private readonly IRepository<Tournament> _repository;
        private readonly IMapper _mapper;

        public TournamentService(
            IMovieService movieService,
            IStandingService standingService,
            IRepository<Tournament> repository,
            IMapper mapper
        )
        {
            _movieService = movieService;
            _standingService = standingService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<string>> CreateAsync(CreateTournamentViewModel model)
        {
            var moviesResponse = await _movieService.GetMoviesAsync();
            if (!moviesResponse.Success)
                return new Response<string>(moviesResponse.Success, moviesResponse.Message);

            var moviesSelected = moviesResponse.Data.Where(m => model.MoviesIds.Contains(m.Id));
            if (moviesSelected.Count() != 8)
                return new Response<string>(false, "Error creating the tournament. Invalid movies Ids.");
            var tournament = new Tournament(_mapper.Map<IEnumerable<Movie>>(moviesSelected));
            await _repository.InsertOneAsync(tournament);

            var standing = await _standingService.CreateAsync(tournament);

            return new Response<string>(true, "Successfully created tournament", standing.Id);
        }
    }
}
