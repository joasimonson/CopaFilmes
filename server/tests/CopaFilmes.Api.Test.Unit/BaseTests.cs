using Bogus;
using CopaFilmes.Api.Settings;
using CopaFilmes.Api.Test.Common.Util;
using Flurl.Http.Testing;

namespace CopaFilmes.Api.Test.Unit
{
    public class BaseTests
    {
        internal static Faker _faker = new Faker();
        internal readonly HttpTest _httpTest;
        internal readonly ApiFilmesSettings _apiFilmesSettings;
        internal readonly SigningSettings _signingSettings;
        internal readonly TokenSettings _tokenSettings;
        internal readonly SystemSettings _systemSettings;

        public BaseTests()
{
            _httpTest = new HttpTest();
            _apiFilmesSettings = ConfigManager.ApiFilmesSettings;
            _signingSettings = ConfigManager.SigningSettings;
            _tokenSettings = ConfigManager.TokenSettings;
            _systemSettings = ConfigManager.SystemSettings;
        }
    }
}
