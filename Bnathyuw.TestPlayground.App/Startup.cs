using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bnathyuw.TestPlayground.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) => 
            services.AddControllers();

        public void Configure(IApplicationBuilder app) =>
            app.UseRouting()
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}