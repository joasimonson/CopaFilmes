using CopaFilmes.Api.Model;
using CopaFilmes.Api.Settings;
using CopaFilmes.Api.Test.Common.Builders;
using CopaFilmes.Api.Test.Common.Util;
using CopaFilmes.Api.Test.Integration.Fixtures;
using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace CopaFilmes.Api.Test.Integration.Specs
{
    [Collection(nameof(ApiTestCollection))]
    public class FilmeControllerTests : IDisposable
    {
        private readonly ApiTokenFixture _apiTokenFixture;
        private readonly SystemSettings _systemSettings;
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;
        private readonly WireMockServer _wireMockServer;
        private readonly IRequestBuilder _request;

        public FilmeControllerTests(ApiTokenFixture apiTokenFixture)
        {
            _apiTokenFixture = apiTokenFixture;
            _systemSettings = ConfigManager.SystemSettings;
            _endpoint = _apiTokenFixture.ConfigRunTests.EndpointFilme;
            _httpClient = _apiTokenFixture.GetAuthenticatedClient().GetAwaiter().GetResult();

            _wireMockServer = WireMockServer.Start(_apiTokenFixture.ConfigRunTests.ServerPort);
            _request = Request
                .Create()
                .WithPath(new WildcardMatcher(ConfigManager.ApiFilmesSettings.EndpointFilmes))
                .UsingGet();
        }

        [Fact]
        public async Task Get_DeveRetornarInternalServerError_QuandoFalhaAoRetornarListaDeFilmes()
        {
            _wireMockServer.Given(_request)
                .RespondWith(Response.Create().WithNotFound());

            var response = await _httpClient.GetAsync(_endpoint);

            response.Should().Be500InternalServerError();
        }

        [Fact]
        public async Task Get_DeveRetornarOk_QuandoRequestForValido()
        {
            var chaveCampeonato = ChaveClassificacaoBuilder.Novo().ComParticipantesFixos().Build();
            var participantes = chaveCampeonato.ObterParticipantes();

            _wireMockServer.Given(_request)
                .RespondWith(Response.Create().WithSuccess().WithBodyAsJson(participantes));

            var response = await _httpClient.GetAsync(_endpoint);

            response.Should().Be200Ok().And.BeAs(participantes);
        }

        [Fact]
        public async Task Get_DeveRetornarCache_QuandoDisponivel()
        {
            var chaveCampeonato = ChaveClassificacaoBuilder.Novo().ComParticipantesFixos().Build();
            var participantes = chaveCampeonato.ObterParticipantes();

            _wireMockServer
                .Given(_request)
                .RespondWith(Response.Create().WithSuccess().WithBodyAsJson(participantes));

            await _httpClient.GetAsync(_endpoint);

            _wireMockServer.Reset();
            _wireMockServer.Given(_request)
                .RespondWith(Response.Create().WithNotFound());

            var responseApiCache = await _httpClient.GetAsync(_endpoint);

            responseApiCache.Should().Be200Ok().And.BeAs(participantes);
        }

        public void Dispose()
        {
            var cacheMemory = _apiTokenFixture.Services.GetService<IMemoryCache>();
            cacheMemory.Remove(_systemSettings.FilmesCacheKey);
            _wireMockServer.Dispose();
        }
    }

    public class FilmeControllerTestsWithMock : ApiTokenFixture
    {
        private readonly SystemSettings _systemSettings;
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        private IMemoryCache MemoryCacheFake;

        public FilmeControllerTestsWithMock()
        {
            _systemSettings = ConfigManager.SystemSettings;
            _endpoint = ConfigRunTests.EndpointFilme;
            _httpClient = GetAuthenticatedClient().GetAwaiter().GetResult();
        }

        protected override void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureTestServices(services);
            MemoryCacheFake = A.Fake<IMemoryCache>();
            services.AddScoped(_ => MemoryCacheFake);
        }

        [Fact]
        public async Task Post_DeveUsarMemoryCache()
        {
            var response = await _httpClient.GetAsync(_endpoint);

            object cache;
            A.CallTo(() => MemoryCacheFake
                .TryGetValue(_systemSettings.FilmesCacheKey, out cache))
                .MustHaveHappened();
        }
    }
}
