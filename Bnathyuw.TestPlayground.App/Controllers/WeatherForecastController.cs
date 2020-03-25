using System.Collections.Generic;
using Bnathyuw.TestPlayground.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bnathyuw.TestPlayground.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        public WeatherForecastController(WeatherService weatherService) => _weatherService = weatherService;

        [HttpGet]
        public IEnumerable<WeatherForecast> Get() => _weatherService.GetWeather();
    }
}