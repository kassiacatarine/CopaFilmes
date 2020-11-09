using System.Collections.Generic;

namespace Championship.Application.ViewModels
{
    public class TournamentViewModel
    {
        public string Id { get; set; }
        public IEnumerable<MovieViewModel> Movies { get; set; }
    }
}
