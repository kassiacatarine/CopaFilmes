using Championship.Domain.AggregatesModel;
using Championship.UnitTests.Builders.Domain;
using FluentAssertions;
using Xunit;

namespace Championship.UnitTests.Domain
{
    public class RankingTest
    {
        [Fact]
        public void TestAddScoreWinner()
        {
            // Arrange
            var movie = new MovieBuilder().Build();
            var ranking = new Ranking(movie);
            // Act
            ranking.AddScoreWinner();
            // Assert
            ranking.Score.Should().Be(1);
        }
    }
}
