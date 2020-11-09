using Championship.Application.ViewModels;
using System.Collections.Generic;

namespace Championship.UnitTests.Builders.Application
{
    public class CreateTournamentViewModelBuilder
    {
        private CreateTournamentViewModel _createTournamentViewModel;
        public List<string> MoviesIds => new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8" };

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

        public CreateTournamentViewModel Build()
        {
            return _createTournamentViewModel;
        }
    }
}
