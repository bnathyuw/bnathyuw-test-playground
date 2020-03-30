using System;
using System.Collections.Generic;
using System.Linq;

namespace Bnathyuw.TestPlayground.App.Services
{
    public interface IWeatherService
    {
        IEnumerable<WeatherForecast> GetWeather();
    }

    public class WeatherService : IWeatherService
    {
        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IClock _clock;

        public WeatherService(IClock clock) => _clock = clock;

        public virtual IEnumerable<WeatherForecast> GetWeather()
        {
            var rng = new Random();
            
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = _clock.Now().AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}