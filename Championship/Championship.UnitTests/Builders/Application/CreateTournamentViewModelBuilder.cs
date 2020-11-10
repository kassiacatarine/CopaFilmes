using Championship.Application.ViewModels;
using System.Collections.Generic;

namespace Championship.UnitTests.Builders.Application
{
    public class CreateTournamentViewModelBuilder
    {
        private CreateTournamentViewModel _createTournamentViewModel;
        public List<string> MoviesIds => new List<string>() { "tt3606756", "tt4881806", "tt5164214", "tt7784604", "tt4154756", "tt5463162", "tt3778644", "tt3501632" };

        public CreateTournamentViewModelBuilder()
        {
            _createTournamentViewModel = WithDefaultValues();
        }

        public CreateTournamentViewModel WithDefaultValues()
        {
            _createTournamentViewModel = new CreateTournamentViewModel()
            {
                MoviesIds = MoviesIds
            };
            return _createTournamentViewModel;
        }

        public CreateTournamentViewModelBuilder WhitMoviesIds(List<string> moviesIds)
        {
            _createTournamentViewModel.MoviesIds = moviesIds;
            return this;
        }

        public CreateTournamentViewModel Build()
        {
            return _createTournamentViewModel;
        }
    }
}
