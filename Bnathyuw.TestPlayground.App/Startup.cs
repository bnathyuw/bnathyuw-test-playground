using Bnathyuw.TestPlayground.App.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Bnathyuw.TestPlayground.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) => 
            services.AddScoped<IWeatherService, WeatherService>()
                .AddScoped<IClock, Clock>()
                .AddControllers();

        public void Configure(IApplicationBuilder app) =>
            app.UseRouting()
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}