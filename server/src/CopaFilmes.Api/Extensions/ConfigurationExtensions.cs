using Microsoft.Extensions.Configuration;

namespace CopaFilmes.Api.Extensions
{
    public static class ConfigurationExtensions
    {
        public static TSettings GetSettings<TSettings>(this IConfiguration configuration) =>
            configuration.GetSection<TSettings>().Get<TSettings>();

        public static IConfigurationSection GetSection<TSettings>(this IConfiguration configuration) =>
            configuration.GetSection(typeof(TSettings).Name);
    }
}
