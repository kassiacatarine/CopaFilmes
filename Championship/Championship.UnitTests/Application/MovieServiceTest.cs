using Championship.Application.Services;
using Championship.Application.ViewModels;
using Championship.Domain.SeedWork;
using FluentAssertions;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Championship.UnitTests.Application
{
    public class MovieServiceTest
    {
        private readonly Mock<HttpMessageHandler> _handlerMock;

        public MovieServiceTest()
        {
            _handlerMock = new Mock<HttpMessageHandler>();
        }

        [Fact]
        public async Task GetMoviesWhitUnavailableServiceAndReturnsUnsuccess()
        {
            // Arrange
            var expected = new Response<IEnumerable<MovieViewModel>>(false, "Error connecting to the movie service");
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            };

            _handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);

            var httpClient = new HttpClient(_handlerMock.Object);
            var service = new MovieService(httpClient);

            // Act
            var result = await service.GetMoviesAsync();

            //Assert
            _handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
               ItExpr.IsAny<CancellationToken>());
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetMoviesWhitAvailableServiceAndReturnsSuccess()
        {
            // Arrange
            string content = File.ReadAllText($"DataTests{Path.DirectorySeparatorChar}Movies.json");
            var data = JsonConvert.DeserializeObject<IEnumerable<MovieViewModel>>(content);
            var json = JsonConvert.SerializeObject(data);
            var expected = new Response<IEnumerable<MovieViewModel>>(true, "Searching for movies successfully", data);
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            _handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);
            var httpClient = new HttpClient(_handlerMock.Object);
            var service = new MovieService(httpClient);

            // Act
            var result = await service.GetMoviesAsync();

            // Assert
            _handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
               ItExpr.IsAny<CancellationToken>());
            result.Should().BeEquivalentTo(expected);
        }
    }
}
