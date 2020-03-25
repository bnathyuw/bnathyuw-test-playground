using System.Net.Http;
using Bnathyuw.TestPlayground.App.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Bnathyuw.TestPlayground.App.Tests
{
    public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private WeatherService _weatherService;

        public TestWebApplicationFactory<TStartup> WithWeatherService(WeatherService weatherService)
        {
            _weatherService = weatherService;
            return this;
        }

        public new HttpClient CreateClient() =>
            WithWebHostBuilder(builder => builder.ConfigureTestServices(ServicesConfiguration)).CreateClient();

        private void ServicesConfiguration(IServiceCollection services) => 
            services.AddScoped(_ => _weatherService);
    }
}