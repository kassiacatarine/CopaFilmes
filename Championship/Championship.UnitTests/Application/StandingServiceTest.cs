using AutoMapper;
using Championship.Application.Services;
using Championship.Application.ViewModels;
using Championship.Domain.AggregatesModel;
using Championship.Domain.SeedWork;
using Championship.Infrastructure.Repositories;
using Championship.UnitTests.Builders.Domain;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Championship.UnitTests.Application
{
    public class StandingServiceTest
    {
        private readonly Mock<IRepository<Standing>> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public StandingServiceTest()
        {
            _repositoryMock = new Mock<IRepository<Standing>>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task TestCreateAsync()
        {
            // Arrange
            string content = File.ReadAllText($"DataTests{Path.DirectorySeparatorChar}StandingViewModel.json");
            var expected = JsonConvert.DeserializeObject<StandingViewModel>(content);
            var tournament = new TournamentBuilder().Build();
            _repositoryMock.Setup(x => x.InsertOneAsync(It.IsAny<Standing>()))
                .Returns(Task.CompletedTask);
            _mapperMock.Setup(x => x.Map<StandingViewModel>(It.IsAny<Standing>()))
                .Returns(expected);
            var service = new StandingService(_repositoryMock.Object, _mapperMock.Object);
            // Act
            var result = await service.CreateAsync(tournament);
            // Assert
            _repositoryMock.Verify(x => x.InsertOneAsync(It.IsAny<Standing>()), Times.Once);
            _mapperMock.Verify(x => x.Map<StandingViewModel>(It.IsAny<Standing>()), Times.Once);
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task TestGetByIdAsyncWhitInvalidIdAndReturnsUnsuccess()
        {
            // Arrange
            var expected = new Response<StandingViewModel>(false, "Standing with the fetched id does not exist");
            _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(It.IsAny<Standing>()));
            var service = new StandingService(_repositoryMock.Object, _mapperMock.Object);
            // Act
            var result = await service.GetByIdAsync(It.IsAny<string>());
            // Assert
            _repositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task TestGetByIdAsyncWhitInvalidIdAndReturnsSuccess()
        {
            // Arrange
            string contentStanding = File.ReadAllText($"DataTests{Path.DirectorySeparatorChar}Standing.json");
            var standing = JsonConvert.DeserializeObject<Standing>(contentStanding);
            string content = File.ReadAllText($"DataTests{Path.DirectorySeparatorChar}StandingViewModel.json");
            var data = JsonConvert.DeserializeObject<StandingViewModel>(content);
            var expected = new Response<StandingViewModel>(true, "Standing search successfully performed", data);
            _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(standing));
            _mapperMock.Setup(x => x.Map<StandingViewModel>(It.IsAny<Standing>()))
                .Returns(data);
            var service = new StandingService(_repositoryMock.Object, _mapperMock.Object);
            // Act
            var result = await service.GetByIdAsync(It.IsAny<string>());
            // Assert
            _repositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
            _mapperMock.Verify(x => x.Map<StandingViewModel>(It.IsAny<Standing>()), Times.Once);
            result.Should().BeEquivalentTo(expected);
        }
    }
}
