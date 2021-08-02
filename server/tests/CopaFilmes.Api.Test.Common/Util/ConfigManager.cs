using CopaFilmes.Api.Settings;
using Microsoft.Extensions.Configuration;

namespace CopaFilmes.Api.Test.Common.Util
{
    internal static class ConfigManager
    {
        internal static readonly IConfiguration Configuration;

        static ConfigManager()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        internal static ApiFilmesSettings ApiFilmesSettings => Configuration.GetSetting<ApiFilmesSettings>();
        internal static SystemSettings SystemSettings => Configuration.GetSetting<SystemSettings>();
        internal static TSetting GetSetting<TSetting>(this IConfiguration configuration) => configuration.GetSection(typeof(TSetting).Name).Get<TSetting>();
    }
}
