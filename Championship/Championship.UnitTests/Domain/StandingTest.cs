using Championship.Domain.AggregatesModel;
using Championship.UnitTests.Builders.Domain;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Championship.UnitTests.Domain
{
    public class StandingTest
    {
        [Fact]
        public void TestGetWinnerMatchWhitNotaEqual()
        {
            // Arrange
            var expected = new MovieBuilder()
                .WithTitulo("Avengers")
                .Build();
            var secondMovie = new MovieBuilder()
                .Build();
            var tournament = new TournamentBuilder().Build();
            var standing = new Standing(tournament);
            // Act
            var result = standing.GetWinnerMatch(expected, secondMovie);
            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void TestGetWinnerMatchWhitNotaDifferent()
        {
            // Arrange
            var expected = new MovieBuilder()
                .WithNota(9M)
                .Build();
            var secondMovie = new MovieBuilder()
                .Build();
            var tournament = new TournamentBuilder().Build();
            var standing = new Standing(tournament);
            // Act
            var result = standing.GetWinnerMatch(expected, secondMovie);
            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void TestGenerateMatchWhitValidTournament()
        {
            // Arrange
            var tournament = new TournamentBuilder().Build();
            var standing = new Standing(tournament);
            var firstMovie = tournament.Movies.First();
            var lastMovie = tournament.Movies.First();
            var expected = standing.GetWinnerMatch(firstMovie, lastMovie);
            // Act
            var result = standing.GenerateMatch(firstMovie, lastMovie);
            // Assert
            standing.Rankings.Count()
                .Should().Be(tournament.Movies.Count());

            standing.Rankings
                .FirstOrDefault(r => r.Movie.Id == expected.Id)
                .Score.Should().Be(1);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task TestCalculateRankingsAsync()
        {
            // Arrange
            var tournament = new TournamentBuilder().Build();
            var standing = new Standing(tournament);
            var movies = tournament.Movies
                .OrderBy(m => m.Titulo);
            var firstHalf = movies.Take(4)
                .ToList();
            var secondHalf = movies
                .Skip(4)
                .Take(4)
                .Reverse()
                .ToList();
            var expectedScore0 = new List<string>() { "tt5164214", "tt7784604", "tt3778644" };
            var expectedScore1 = new List<string>() { "tt3501632", "tt4881806" };
            var expectedScore2 = new List<string>() { "tt3606756" };
            var expectedScore3 = new List<string>() { "tt4154756" };

            // Act
            var result = await standing.CalculateRankingsAsync(firstHalf, secondHalf);
            // Assert
            result.Should().BeTrue();
            standing.Rankings
                .Where(r => expectedScore0.Contains(r.Movie.Id))
                .ToList()
                .Should().HaveCount(expectedScore0.Count);
            
            standing.Rankings
                .Where(r => expectedScore1.Contains(r.Movie.Id))
                .ToList()
                .Should().HaveCount(expectedScore1.Count);

            standing.Rankings
                .Where(r => expectedScore2.Contains(r.Movie.Id))
                .ToList()
                .Should().HaveCount(expectedScore2.Count);

            standing.Rankings
                .Where(r => expectedScore3.Contains(r.Movie.Id))
                .ToList()
                .Should().HaveCount(expectedScore3.Count);
        }

        [Fact]
        public async Task TestRunMatchesAsync()
        {
            // Arrange
            var tournament = new TournamentBuilder().Build();
            var standing = new Standing(tournament);
            var expectedScore0 = new List<string>() { "tt5164214", "tt7784604", "tt3778644" };
            var expectedScore1 = new List<string>() { "tt3501632", "tt4881806" };
            var expectedScore2 = new List<string>() { "tt3606756" };
            var expectedScore3 = new List<string>() { "tt4154756" };

            // Act
            await standing.RunMatchesAsync();
            // Assert
            standing.Rankings
                .Where(r => expectedScore0.Contains(r.Movie.Id))
                .ToList()
                .Should().HaveCount(expectedScore0.Count);

            standing.Rankings
                .Where(r => expectedScore1.Contains(r.Movie.Id))
                .ToList()
                .Should().HaveCount(expectedScore1.Count);

            standing.Rankings
                .Where(r => expectedScore2.Contains(r.Movie.Id))
                .ToList()
                .Should().HaveCount(expectedScore2.Count);

            standing.Rankings
                .Where(r => expectedScore3.Contains(r.Movie.Id))
                .ToList()
                .Should().HaveCount(expectedScore3.Count);
        }
    }
}
