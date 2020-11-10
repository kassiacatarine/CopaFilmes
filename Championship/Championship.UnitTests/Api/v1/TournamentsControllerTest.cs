using Championship.API.Controllers.v1;
using Championship.Application.Services;
using Championship.Domain.SeedWork;
using Championship.UnitTests.Builders.Application;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Championship.UnitTests.Api.v1
{
    public class TournamentsControllerTest
    {
        private readonly Mock<ITournamentService> _tournamentServiceMock;
        public TournamentsControllerTest()
        {
            _tournamentServiceMock = new Mock<ITournamentService>();
        }

        [Fact]
        public async Task TestCreateTournamentWhitInvalidsMoviesAndReturnsBadRequest()
        {
            // Arrange
            var model = new CreateTournamentViewModelBuilder().Build();
            var expected = new Response<string>(false, "Error creating the tournament. Invalid movies Ids.");
            _tournamentServiceMock.Setup(x => x.CreateAsync(model))
                .Returns(Task.FromResult(expected));

            var controller = new TournamentsController(_tournamentServiceMock.Object);
            // Act
            var actionResult = await controller.PostAsync(model);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            badRequestResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            var result = Assert.IsType<Response<string>>(badRequestResult.Value);
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task TestCreateTournamentWhitUnavailableServiceMoviesAndReturnsBadRequest()
        {
            // Arrange
            var model = new CreateTournamentViewModelBuilder().Build();
            var expected = new Response<string>(false, "Error connecting to the movie service");
            _tournamentServiceMock.Setup(x => x.CreateAsync(model))
                .Returns(Task.FromResult(expected));

            var controller = new TournamentsController(_tournamentServiceMock.Object);
            // Act
            var actionResult = await controller.PostAsync(model);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            badRequestResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            var result = Assert.IsType<Response<string>>(badRequestResult.Value);
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task TestCreateTournamentWhitValidsMoviesAndReturnsCreated()
        {
            // Arrange
            var model = new CreateTournamentViewModelBuilder().Build();
            var expected = new Response<string>(true, "Successfully created tournament", "12345");
            _tournamentServiceMock.Setup(x => x.CreateAsync(model))
                .Returns(Task.FromResult(expected));

            var controller = new TournamentsController(_tournamentServiceMock.Object);
            // Act
            var actionResult = await controller.PostAsync(model);

            //Assert
            var createdRequestResult = Assert.IsType<CreatedResult>(actionResult.Result);
            createdRequestResult.StatusCode.Should().Be((int)HttpStatusCode.Created);
            var result = Assert.IsType<Response<string>>(createdRequestResult.Value);
            result.Should().BeEquivalentTo(expected);
        }
    }
}
