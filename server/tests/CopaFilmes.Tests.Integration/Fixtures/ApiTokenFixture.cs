using AutoBogus;
using CopaFilmes.Api.Extensions;
using CopaFilmes.Api.Servicos.Login;
using CopaFilmes.Api.Settings;
using CopaFilmes.Tests.Common.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CopaFilmes.Tests.Integration.Fixtures
{
    public class ApiTokenFixture : BaseFixture
    {
        private readonly LoginRequest _loginRequest;
        private readonly HttpClient _client;

        public ApiTokenFixture()
        {
            var accessKey = GetConfiguration().GetSection("AccessKey").Value;
            _loginRequest = new AutoFaker<LoginRequest>().RuleFor(l => l.Senha, accessKey).Generate();
            _client = GetDefaultHttpClient();
        }

        protected override void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureTestServices(services);
            var config = GetTestConfiguration();
            var opt = Options.Create(config.GetSettings<ApiFilmesSettings>());
            services.AddScoped(_ => opt);
        }

        public async Task<HttpClient> GetAuthenticatedClient()
        {
            var result = await CreateAccessToken().ConfigureAwait(false);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
            return _client;
        }

        private async Task<LoginResult> CreateAccessToken()
        {
            var response = await _client.PostAsync(ConfigRunTests.EndpointLogin, _loginRequest.AsHttpContent());
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var login = JsonConvert.DeserializeObject<LoginResult>(jsonResponse);
            return login;
        }
    }
}
