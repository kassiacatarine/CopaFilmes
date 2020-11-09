using System.Collections.Generic;

namespace Championship.Domain.AggregatesModel
{
    public class Standings
    {
        public string Id { get; set; }
        public string TournamentId { get; set; }
        public Tournament Tournament { get; set; }
        public IEnumerable<Ranking> Rankings { get; set; }
    }
}
