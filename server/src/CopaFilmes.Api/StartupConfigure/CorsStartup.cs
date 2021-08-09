using CopaFilmes.Api.Extensions;
using CopaFilmes.Api.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CopaFilmes.Api.StartupConfigure
{
    public static class CorsStartup
    {
        public static void Configurar(IServiceCollection services, IConfiguration configuration)
        {
            var systemSettings = configuration.GetSettings<SystemSettings>();
            
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins(systemSettings.UrlWeb)
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }
    }
}
