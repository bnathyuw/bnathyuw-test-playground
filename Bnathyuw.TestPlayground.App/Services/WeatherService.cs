using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;

namespace Bnathyuw.TestPlayground.App.Services
{
    public interface IWeatherService
    {
        IEnumerable<WeatherForecast> GetWeather();
    }

    public class WeatherService : IWeatherService
    {
        private readonly string[] _summaries;

        private readonly IClock _clock;

        public WeatherService(IClock clock, IOptionsMonitor<WeatherOptions> weatherOptions)
        {
            _clock = clock;
            _summaries = weatherOptions.CurrentValue.Summaries;
        }

        public virtual IEnumerable<WeatherForecast> GetWeather()
        {
            var rng = new Random();
            
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = _clock.Now().AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = _summaries[rng.Next(_summaries.Length)]
                })
                .ToArray();
        }
    }
}