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

        internal static ApiFilmesSettings ApiFilmesSettings => GetSection<ApiFilmesSettings>();
        internal static SystemSettings SystemSettings => GetSection<SystemSettings>();
        internal static TSetting GetSection<TSetting>() => Configuration.GetSection(typeof(TSetting).Name).Get<TSetting>();
    }
}
