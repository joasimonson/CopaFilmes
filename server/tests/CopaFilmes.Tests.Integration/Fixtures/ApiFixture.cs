﻿using CopaFilmes.Api.Servicos.Login;
using CopaFilmes.Api.Servicos.Usuario;
using CopaFilmes.Api.Wrappers.MemoryCache;
using CopaFilmes.Tests.Common.Builders;
using CopaFilmes.Tests.Common.Util;
using FakeItEasy;
using Microsoft.Extensions.Caching.Memory;
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
        public UsuarioRequest Usuario { get; private set; }
        public LoginRequest Login { get; private set; }
        public LoginResult LoginToken { get; private set; }
        public MemoryCacheWrapper MemoryCacheWrapperFake { get; private set; }

        private readonly DatabaseFixture _databaseFixture = new();
        private readonly HttpClient _httpClient;

        public ApiFixture()
        {
            _httpClient = GetHttpClient();
        }

        protected override void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureTestServices(services);
            IMemoryCache memoryCache = new MemoryCache(Options.Create(new MemoryCacheOptions() { }));
            MemoryCacheWrapperFake = A.Fake<MemoryCacheWrapper>(opt => opt.WithArgumentsForConstructor(new object[] { memoryCache }).CallsBaseMethods());
            services.AddSingleton(_ => memoryCache);
            services.AddSingleton(_ => MemoryCacheWrapperFake);
        }

        public async Task Initializar()
        {
            await _databaseFixture.Reset(new[] { "TS_USUARIO" });
            Usuario = new()
            {
                Usuario = UtilFaker.FakerHub.Person.FirstName,
                Senha = UtilFaker.FakerHub.Random.AlphaNumeric(8)
            };
            Login = new()
            {
                Usuario = Usuario.Usuario,
                Senha = Usuario.Senha
            };

            await _httpClient.PostAsync(ConfigRunTests.EndpointUsuario, Usuario.AsHttpContent());

            var responseLogin = await _httpClient.PostAsync(ConfigRunTests.EndpointLogin, Login.AsHttpContent());
            var jsonResponseLogin = await responseLogin.Content.ReadAsStringAsync();
            LoginToken = JsonConvert.DeserializeObject<LoginResult>(jsonResponseLogin);
        }

        public HttpClient GetAuthHttpClient()
        {
            var client = GetHttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginToken.Token);
            return client;
        }

        public async Task<UsuarioResult> CriarUsuarioNoBanco()
        {
            var response = await _httpClient.PostAsync(ConfigRunTests.EndpointUsuario, Usuario.AsHttpContent());
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UsuarioResult>(jsonResponse);
            return result;
        }
    }
}
