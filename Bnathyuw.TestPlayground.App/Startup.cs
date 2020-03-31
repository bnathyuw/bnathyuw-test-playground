using Bnathyuw.TestPlayground.App.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bnathyuw.TestPlayground.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => _configuration = configuration;

        private readonly IConfiguration _configuration;
 
        public void ConfigureServices(IServiceCollection services) => 
            services.AddScoped<IWeatherService, WeatherService>()
                .AddScoped<IClock, Clock>()
                .Configure<WeatherOptions>(_configuration.GetSection("Weather"))
                .AddControllers();

        public void Configure(IApplicationBuilder app) =>
            app.UseRouting()
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}