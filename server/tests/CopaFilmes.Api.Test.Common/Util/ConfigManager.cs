using CopaFilmes.Api.Extensions;
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
                .AddJsonFile("appsettings.common.json")
                .Build();
        }

        internal static ApiFilmesSettings ApiFilmesSettings => Configuration.GetSettings<ApiFilmesSettings>();
        internal static SystemSettings SystemSettings => Configuration.GetSettings<SystemSettings>();
        internal static TokenSettings TokenSettings => Configuration.GetSettings<TokenSettings>();
        internal static SigningSettings SigningSettings = new SigningSettings();
    }
}
