using System;
using System.Linq;
using AutoFixture.Xunit2;
using Bnathyuw.TestPlayground.App.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Bnathyuw.TestPlayground.App.Tests
{
    public class WeatherServiceShould
    {
        private static readonly string[] Descriptions =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IClock _clock;
        private readonly WeatherService _weatherService;

        public WeatherServiceShould()
        {
            _clock = Substitute.For<IClock>();
            _weatherService = new WeatherService(_clock);
        }

        [Theory, AutoData]
        public void CreateForecastsForTheNextFiveDays(DateTime now)
        {
            _clock.Now().Returns(now);

            var forecasts = _weatherService.GetWeather();

            forecasts.Should().BeEquivalentTo(
                    new {Date = now.AddDays(1)},
                    new {Date = now.AddDays(2)},
                    new {Date = now.AddDays(3)},
                    new {Date = now.AddDays(4)},
                    new {Date = now.AddDays(5)});
        }
        
        [Fact]
        public void CreateForecastsWhoseTemperatureIsWithinReasonableBounds()
        {
            var forecasts = _weatherService.GetWeather();

            forecasts.Should().OnlyContain(f => -20 <= f.TemperatureC && f.TemperatureC <= 55);
        }

        [Fact]
        public void CreateForecastsWithSummariesChosenFromAKnownList()
        {
            var forecasts = _weatherService.GetWeather();

            forecasts.Should().OnlyContain(f => Descriptions.Contains(f.Summary));
        }

    }
}