using Championship.API.Controllers.v1;
using Championship.Application.Services;
using Championship.Application.ViewModels;
using Championship.Domain.SeedWork;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
        public async Task GetMoviesWhitUnavailableServiceAndReturnsBadRequest()
        {
            // Arrange
            var expected = new Response<IEnumerable<MovieViewModel>>(false, "Error connecting to the movie service", It.IsAny<IEnumerable<MovieViewModel>>());
            _movieServiceMock.Setup(x => x.GetMoviesAsync())
                .Returns(Task.FromResult(expected));

            var controller = new MoviesController(_movieServiceMock.Object);
            // Act
            var actionResult = await controller.GetAsync();

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            badRequestResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            var result = Assert.IsType<Response<IEnumerable<MovieViewModel>>>(badRequestResult.Value);
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetMoviesWhitAvailableServiceAndReturnsOK()
        {
            // Arrange
            string content = File.ReadAllText($"DataTests{Path.DirectorySeparatorChar}Movies.json");
            var data = JsonConvert.DeserializeObject<IEnumerable<MovieViewModel>>(content);
            var expected = new Response<IEnumerable<MovieViewModel>>(true, "Searching for movies successfully", data);
            _movieServiceMock.Setup(x => x.GetMoviesAsync())
                .Returns(Task.FromResult(expected));

            var controller = new MoviesController(_movieServiceMock.Object);
            // Act
            var actionResult = await controller.GetAsync();

            //Assert
            var createdRequestResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            createdRequestResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var result = Assert.IsType<Response<IEnumerable<MovieViewModel>>>(createdRequestResult.Value);
            result.Should().BeEquivalentTo(expected);
        }
    }
}
