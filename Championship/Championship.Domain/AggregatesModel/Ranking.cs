namespace Championship.Domain.AggregatesModel
{
    public class Ranking
    {
        public string Id { get; set; }
        public int Position { get; set; }
        public string MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
