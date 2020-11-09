using Championship.Application.Services;
using Championship.Application.ViewModels;
using FluentAssertions;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public async Task TestGetMoviesAndReturnMovies()
        {
            // Arrange
            string content = File.ReadAllText($"DataTests{Path.DirectorySeparatorChar}Movies.json");
            var expected = JsonConvert.DeserializeObject<IEnumerable<MovieViewModel>>(content);
            var json = JsonConvert.SerializeObject(expected);
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
            Assert.NotNull(result);
            _handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
               ItExpr.IsAny<CancellationToken>());
            result.Should().BeEquivalentTo(expected);
        }
    }
}
