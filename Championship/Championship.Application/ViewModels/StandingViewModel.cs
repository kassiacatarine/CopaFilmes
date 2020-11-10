using System.Collections.Generic;

namespace Championship.Application.ViewModels
{
    public class StandingViewModel
    {
        public string Id { get; set; }
        public string TournamentId { get; set; }
        public IEnumerable<RankingViewModel> Rankings { get; set; }
    }
}
