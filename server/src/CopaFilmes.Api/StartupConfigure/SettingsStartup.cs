using CopaFilmes.Api.Extensions;
using CopaFilmes.Api.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CopaFilmes.Api.StartupConfigure
{
    public static class SettingsStartup
    {
        public static void Configurar(IServiceCollection services, IConfiguration configuration)
        {
            services.BindSettings<ApiFilmesSettings>(configuration);
            services.BindSettings<SystemSettings>(configuration);
            services.BindSettings<TokenSettings>(configuration);
        }

        private static IServiceCollection BindSettings<TSettings>(this IServiceCollection services, IConfiguration configuration) where TSettings : class
        {
            var settings = configuration.GetSection<TSettings>();
            services.Configure<TSettings>(settings);
            return services;
        }
    }
}
