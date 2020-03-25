using System.Net.Http;
using System.Threading.Tasks;
using Bnathyuw.TestPlayground.App;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using static System.Net.HttpStatusCode;

namespace Bnathyuw.TestPlayground.Tests
{
    public class WeatherForecastShould : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public WeatherForecastShould(WebApplicationFactory<Startup> factory) => _client = factory.CreateClient();

        [Fact]
        public async Task Exist()
        {
            var response = await _client.GetAsync("/weatherforecast");

            response.Should().BeEquivalentTo(new {StatusCode = OK});
        }
    }
}