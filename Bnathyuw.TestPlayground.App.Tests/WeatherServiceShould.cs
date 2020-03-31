using System;
using System.Linq;
using AutoFixture.Xunit2;
using Bnathyuw.TestPlayground.App.Services;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Bnathyuw.TestPlayground.App.Tests
{
    public class WeatherServiceShould
    {
        private const int LowestExpectedTemperature = -20;
        private const int HighestExpectedTemperature = 55;

        private static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IClock _clock;
        private readonly WeatherService _weatherService;

        public WeatherServiceShould()
        {
            _clock = Substitute.For<IClock>();
            var weatherOptions = Substitute.For<IOptionsMonitor<WeatherOptions>>();
            weatherOptions.CurrentValue.Returns(new WeatherOptions {Summaries = Summaries});
            _weatherService = new WeatherService(_clock, weatherOptions);
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

            forecasts.Should().OnlyContain(f =>
                LowestExpectedTemperature <= f.TemperatureC
                && f.TemperatureC <= HighestExpectedTemperature);
        }

        [Fact]
        public void CreateForecastsWithSummariesChosenFromAKnownList()
        {
            var forecasts = _weatherService.GetWeather();

            forecasts.Should().OnlyContain(f =>
                Summaries.Contains(f.Summary));
        }
    }
}