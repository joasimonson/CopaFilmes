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

        public ApiTokenFixture()
        {
            LoginRequest = new AutoFaker<LoginRequest>().RuleFor(l => l.Senha, Configuration.GetSection("AccessKey").Value).Generate();
        }

        protected override void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureTestServices(services);
            var config = GetTestConfiguration();
            var opt = Options.Create(config.GetSettings<ApiFilmesSettings>());
            services.AddScoped(_ => opt);
        }

        public async Task<HttpClient> CreateAuthenticatedClient()
        {
            var result = await CreateAccessToken();
            var client = CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
            return client;
        }

        private async Task<LoginResult> CreateAccessToken()
        {
            var response = await HttpClient.PostAsync(ConfigRunTests.EndpointLogin, LoginRequest.AsHttpContent());
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var login = JsonConvert.DeserializeObject<LoginResult>(jsonResponse);
            return login;
        }
    }
}
