using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Bnathyuw.TestPlayground.App.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Bnathyuw.TestPlayground.App.Tests
{
    public class WeatherForecastControllerShould : IClassFixture<TestWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly IWeatherService _weatherService;

        public WeatherForecastControllerShould(TestWebApplicationFactory<Startup> factory)
        {
            _weatherService = Substitute.For<IWeatherService>();
            _client = factory.WithWeatherService(_weatherService).CreateClient();
        }

        [Theory, AutoData]
        public async Task ReturnForecastFromWeatherService(WeatherForecast[] weatherForecast)
        {
            _weatherService.GetWeather().Returns(weatherForecast);
            
            var response = await _client.GetAsync("/weatherforecast");

            (await response.Content.ReadAsAsync<WeatherForecast[]>())
                .Should()
                .BeEquivalentTo(weatherForecast);
        }
    }
}