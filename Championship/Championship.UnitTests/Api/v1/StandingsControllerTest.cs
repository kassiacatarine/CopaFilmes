using Championship.API.Controllers.v1;
using Championship.Application.Services;
using Championship.Application.ViewModels;
using Championship.Domain.SeedWork;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Championship.UnitTests.Api.v1
{
    public class StandingsControllerTest
    {
        private readonly Mock<IStandingService> _standingServiceMock;

        public StandingsControllerTest()
        {
            _standingServiceMock = new Mock<IStandingService>();
        }

        [Fact]
        public async Task GetStandingWhitInvalidIdAndReturnsBadRequest()
        {
            // Arrange
            var expected = new Response<StandingViewModel>(false, "Standing with the fetched id does not exist");
            _standingServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(expected));

            var controller = new StandingsController(_standingServiceMock.Object);
            // Act
            var actionResult = await controller.GetAsync(It.IsAny<string>());

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            badRequestResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            var result = Assert.IsType<Response<StandingViewModel>>(badRequestResult.Value);
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetMoviesWhitAvailableServiceAndReturnsOK()
        {
            // Arrange
            string content = File.ReadAllText($"DataTests{Path.DirectorySeparatorChar}StandingViewModel.json");
            var data = JsonConvert.DeserializeObject<StandingViewModel>(content);
            var expected = new Response<StandingViewModel>(true, "Standing search successfully performed", data);
            _standingServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(expected));

            var controller = new StandingsController(_standingServiceMock.Object);
            // Act
            var actionResult = await controller.GetAsync(It.IsAny<string>());

            //Assert
            var createdRequestResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            createdRequestResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var result = Assert.IsType<Response<StandingViewModel>>(createdRequestResult.Value);
            result.Should().BeEquivalentTo(expected);
        }
    }
}
