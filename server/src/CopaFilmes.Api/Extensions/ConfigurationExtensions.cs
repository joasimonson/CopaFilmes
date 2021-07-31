using Microsoft.Extensions.Configuration;

namespace CopaFilmes.Api.Extensions
{
    public static class ConfigurationExtensions
    {
        public static TSettings GetSettings<TSettings>(this IConfiguration configuration) =>
            configuration.GetSection(typeof(TSettings).Name).Get<TSettings>();
    }
}
