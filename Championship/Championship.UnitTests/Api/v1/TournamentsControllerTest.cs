using Championship.API.Controllers.v1;
using Championship.Application.Services;
using Championship.Application.ViewModels;
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
        public async Task CreateTournamentWhitInvalidsMoviesAndReturnsBadRequest()
        {
            // Arrange
            var model = new CreateTournamentViewModelBuilder().Build();
            var expected = new Response<TournamentViewModel>(false, "Error creating the tournament", It.IsAny<TournamentViewModel>());
            _tournamentServiceMock.Setup(x => x.CreateAsync(model))
                .Returns(Task.FromResult(expected));

            var controller = new TournamentsController(_tournamentServiceMock.Object);
            // Act
            var actionResult = await controller.PostAsync(model);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            badRequestResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            var result = Assert.IsType<Response<TournamentViewModel>>(badRequestResult.Value);
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task CreateTournamentWhitValidsMoviesAndReturnsCreated()
        {
            // Arrange
            var model = new CreateTournamentViewModelBuilder().Build();
            var expected = new Response<TournamentViewModel>(true, "Successfully created tournament", It.IsAny<TournamentViewModel>());
            _tournamentServiceMock.Setup(x => x.CreateAsync(model))
                .Returns(Task.FromResult(expected));

            var controller = new TournamentsController(_tournamentServiceMock.Object);
            // Act
            var actionResult = await controller.PostAsync(model);

            //Assert
            var createdRequestResult = Assert.IsType<CreatedResult>(actionResult.Result);
            createdRequestResult.StatusCode.Should().Be((int)HttpStatusCode.Created);
            var result = Assert.IsType<Response<TournamentViewModel>>(createdRequestResult.Value);
            result.Should().BeEquivalentTo(expected);
        }
    }
}
