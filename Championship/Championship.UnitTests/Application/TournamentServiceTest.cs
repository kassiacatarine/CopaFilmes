using AutoMapper;
using Championship.Application.Services;
using Championship.Application.ViewModels;
using Championship.Domain.AggregatesModel;
using Championship.Domain.SeedWork;
using Championship.Infrastructure.Repositories;
using Championship.UnitTests.Builders.Application;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Championship.UnitTests.Application
{
    public class TournamentServiceTest
    {
        private readonly Mock<IMovieService> _movieServiceMock;
        private readonly Mock<IStandingService> _standingServiceMock;
        private readonly Mock<IRepository<Tournament>> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public TournamentServiceTest()
        {
            _movieServiceMock = new Mock<IMovieService>();
            _standingServiceMock = new Mock<IStandingService>();
            _repositoryMock = new Mock<IRepository<Tournament>>();
            _mapperMock = new Mock<IMapper>();
        }
        
        [Fact]
        public async Task TestCreateAsyncWhitUnavailableServiceMovieAndReturnsUnsuccess()
        {
            // Arrange
            var expected = new Response<string>(false, "Error connecting to the movie service");

            var responseMovie = new Response<IEnumerable<MovieViewModel>>(false, "Error connecting to the movie service");
            _movieServiceMock.Setup(x => x.GetMoviesAsync())
                .Returns(Task.FromResult(responseMovie));

            var service = new TournamentService(_movieServiceMock.Object, _standingServiceMock.Object, _repositoryMock.Object, _mapperMock.Object);
            // Act
            var result = await service.CreateAsync(It.IsAny<CreateTournamentViewModel>());
            // Assert
            _movieServiceMock.Verify(x => x.GetMoviesAsync(), Times.Once);
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task TestCreateAsyncWhitInvalidsMoviesAndReturnsUnsuccess()
        {
            // Arrange
            var model = new CreateTournamentViewModelBuilder()
                .WhitMoviesIds(new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8" })
                .Build();
            var expected = new Response<string>(false, "Error creating the tournament. Invalid movies Ids.");

            string content = File.ReadAllText($"DataTests{Path.DirectorySeparatorChar}Movies.json");
            var data = JsonConvert.DeserializeObject<IEnumerable<MovieViewModel>>(content);
            var responseMovie = new Response<IEnumerable<MovieViewModel>>(true, It.IsAny<string>(), data);
            _movieServiceMock.Setup(x => x.GetMoviesAsync())
                .Returns(Task.FromResult(responseMovie));

            var service = new TournamentService(_movieServiceMock.Object, _standingServiceMock.Object, _repositoryMock.Object, _mapperMock.Object);
            // Act
            var result = await service.CreateAsync(model);
            // Assert
            _movieServiceMock.Verify(x => x.GetMoviesAsync(), Times.Once);
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task TestCreateAsyncWhitValidsMoviesAndReturnsSuccess()
        {
            // Arrange
            var model = new CreateTournamentViewModelBuilder().Build();
            var expected = new Response<string>(true, "Successfully created tournament", "12345");

            string contentMovies = File.ReadAllText($"DataTests{Path.DirectorySeparatorChar}Movies.json");
            var movies = JsonConvert.DeserializeObject<IEnumerable<MovieViewModel>>(contentMovies);
            var responseMovie = new Response<IEnumerable<MovieViewModel>>(true, It.IsAny<string>(), movies);
            _movieServiceMock.Setup(x => x.GetMoviesAsync())
                .Returns(Task.FromResult(responseMovie));

            _repositoryMock.Setup(x => x.InsertOneAsync(It.IsAny<Tournament>()))
                .Returns(Task.CompletedTask);

            string contentStanding = File.ReadAllText($"DataTests{Path.DirectorySeparatorChar}StandingViewModel.json");
            var standing = JsonConvert.DeserializeObject<StandingViewModel>(contentStanding);
            _standingServiceMock.Setup(x => x.CreateAsync(It.IsAny<Tournament>()))
                .Returns(Task.FromResult(standing));

            var service = new TournamentService(_movieServiceMock.Object, _standingServiceMock.Object, _repositoryMock.Object, _mapperMock.Object);
            // Act
            var result = await service.CreateAsync(model);
            // Assert
            _movieServiceMock.Verify(x => x.GetMoviesAsync(), Times.Once);
            _repositoryMock.Verify(x => x.InsertOneAsync(It.IsAny<Tournament>()), Times.Once);
            _standingServiceMock.Verify(x => x.CreateAsync(It.IsAny<Tournament>()), Times.Once);
            result.Should().BeEquivalentTo(expected);
        }
    }
}
