using AutoBogus;
using CopaFilmes.Api.Extensions;
using CopaFilmes.Api.Servicos.Login;
using CopaFilmes.Api.Settings;
using CopaFilmes.Api.Test.Common.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Test.Integration.Fixtures
{
    public class ApiTokenFixture : BaseFixture
    {
        public readonly LoginRequest LoginRequest;
        private readonly HttpClient _client;

        public ApiTokenFixture()
        {
            LoginRequest = new AutoFaker<LoginRequest>().RuleFor(l => l.Senha, Configuration.GetSection("AccessKey").Value).Generate();
            _client = CreateClient();
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
            var result = await CreateAccessToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
            return _client;
        }

        private async Task<LoginResult> CreateAccessToken()
        {
            var response = await HttpClient.PostAsync(ConfigRunTests.EndpointLogin, LoginRequest.AsHttpContent());
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var login = JsonConvert.DeserializeObject<LoginResult>(jsonResponse);
            return login;
        }

        protected override void Dispose(bool disposing)
        {
            _client.Dispose();
            base.Dispose(disposing);
        }
    }
}
