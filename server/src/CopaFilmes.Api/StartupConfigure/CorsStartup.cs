using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CopaFilmes.Api.StartupConfigure
{
    public static class CorsStartup
    {
        public static void Configurar(IServiceCollection services, IConfiguration configuration)
        {
            string urlWeb = configuration.GetValue<string>("UrlWeb");

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins(urlWeb)
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }
    }
}
