using Championship.API.Controllers.v1;
using Championship.Application.Services;
using Championship.Application.ViewModels;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Championship.UnitTests.Api.v1
{
    public class MoviesControllerTest
    {
        private readonly Mock<IMovieService> _movieServiceMock;

        public MoviesControllerTest()
        {
            _movieServiceMock = new Mock<IMovieService>();
        }

        [Fact]
        public async Task TestGetMoviesAndReturnMovies()
        {
            // Arrange
            string content = File.ReadAllText($"DataTests{Path.DirectorySeparatorChar}Movies.json");
            var expected = JsonConvert.DeserializeObject<IEnumerable<MovieViewModel>>(content);

            _movieServiceMock.Setup(x => x.GetMoviesAsync())
                .Returns(Task.FromResult(expected));

            //Act
            var moviesController = new MoviesController(_movieServiceMock.Object);
            var result = await moviesController.GetAsync();

            //Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}
