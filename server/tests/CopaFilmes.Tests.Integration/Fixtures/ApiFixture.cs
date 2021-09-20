using AutoBogus;
using CopaFilmes.Api.Extensions;
using CopaFilmes.Api.Servicos.Login;
using CopaFilmes.Api.Servicos.Usuario;
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
    public class ApiFixture : BaseFixture
    {
        private readonly UsuarioRequest _usuarioRequest;
        private readonly LoginRequest _loginRequest;
        private readonly HttpClient _client;

        public ApiFixture()
        {
            _usuarioRequest = new AutoFaker<UsuarioRequest>().Generate();
            _loginRequest = new LoginRequest()
            {
                Usuario = _usuarioRequest.Usuario,
                Senha = _usuarioRequest.Senha
            };
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
            var result = await CriarAccessToken().ConfigureAwait(false);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
            return _client;
        }

        public UsuarioRequest GetUsuarioRequest => _usuarioRequest;

        public async Task<UsuarioResult> CriarUsuarioNoBanco()
        {
            var response = await _client.PostAsync(ConfigRunTests.EndpointUsuario, _usuarioRequest.AsHttpContent());
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UsuarioResult>(jsonResponse);
            return result;
        }

        private async Task<LoginResult> CriarAccessToken()
        {
            var response = await _client.PostAsync(ConfigRunTests.EndpointLogin, _loginRequest.AsHttpContent());
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var login = JsonConvert.DeserializeObject<LoginResult>(jsonResponse);
            return login;
        }
    }
}
