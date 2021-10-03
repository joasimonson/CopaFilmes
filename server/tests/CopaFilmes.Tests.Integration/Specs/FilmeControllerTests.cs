using CopaFilmes.Api.Model;
using CopaFilmes.Api.Settings;
using CopaFilmes.Tests.Common.Builders;
using CopaFilmes.Tests.Common.Util;
using CopaFilmes.Tests.Integration.Fixtures;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace CopaFilmes.Tests.Integration.Specs
{
    [Collection(nameof(ApiTestCollection))]
    public class FilmeControllerTests : IDisposable
    {
        private readonly ApiFixture _apiFixture;
        private readonly SystemSettings _systemSettings;
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;
        private readonly WireMockServer _wireMockServer;
        private readonly IRequestBuilder _request;

        public FilmeControllerTests(ApiFixture apiFixture)
        {
            apiFixture.Initialize().GetAwaiter().GetResult();

            _apiFixture = apiFixture;
            _httpClient = _apiFixture.GetAuthHttpClient();
            _endpoint = ConfigManagerIntegration.ConfigRunTests.EndpointFilme;
            _systemSettings = ConfigManager.SystemSettings;

            _wireMockServer = WireMockServer.Start(ConfigManagerIntegration.ConfigRunTests.ServerPort);
            _request = Request
                .Create()
                .WithPath(new WildcardMatcher(ConfigManager.ApiFilmesSettings.EndpointFilmes))
                .UsingGet();

            IEnumerable<FilmeModel> cache;
            A.CallTo(() => _apiFixture.MemoryCacheWrapperFake
                .TryGetValue(_systemSettings.FilmesCacheKey, out cache))
                .Returns(false);

            A.CallTo(() => _apiFixture.MemoryCacheWrapperFake
                .GetOrCreateAsync(_systemSettings.FilmesCacheKey, A<Func<ICacheEntry, Task<IEnumerable<FilmeModel>>>>.Ignored))
                .CallsBaseMethod();
        }

        [Fact]
        public async Task Get_DeveRetornarOk_QuandoRequestForValido()
        {
            //Arrange
            var chaveCampeonato = ChaveClassificacaoBuilder.Novo().ComParticipantesFixos().Build();
            var participantes = chaveCampeonato.ObterParticipantes();

            _wireMockServer
                .Given(_request)
                .RespondWith(Response.Create().WithSuccess().WithBodyAsJson(participantes));

            //Act
            var response = await _httpClient.GetAsync(_endpoint);

            //Assert
            response.Should().Be200Ok().And.BeAs(participantes);
        }

        [Fact]
        public async Task Get_DeveUsarMemoryCache()
        {
            //Arrange
            A.CallTo(() => _apiFixture.MemoryCacheWrapperFake
                .GetOrCreateAsync(_systemSettings.FilmesCacheKey, A<Func<ICacheEntry, Task<IEnumerable<FilmeModel>>>>.Ignored))
                .Returns(Task.FromResult(default(IEnumerable<FilmeModel>)));

            //Act
            await _httpClient.GetAsync(_endpoint);

            //Assert
            A.CallTo(() => _apiFixture.MemoryCacheWrapperFake
                .GetOrCreateAsync(_systemSettings.FilmesCacheKey, A<Func<ICacheEntry, Task<IEnumerable<FilmeModel>>>>.Ignored))
                .MustHaveHappened();
        }

        [Fact]
        public async Task Get_DeveRetornarCache_QuandoDisponivel()
        {
            //Arrange
            IEnumerable<FilmeModel> cache;
            A.CallTo(() => _apiFixture.MemoryCacheWrapperFake
                .TryGetValue(_systemSettings.FilmesCacheKey, out cache))
                .CallsBaseMethod();

            var chaveCampeonato = ChaveClassificacaoBuilder.Novo().ComParticipantesFixos().Build();
            var participantes = chaveCampeonato.ObterParticipantes();

            _wireMockServer
                .Given(_request)
                .RespondWith(Response.Create().WithSuccess().WithBodyAsJson(participantes));

            //Act
            await _httpClient.GetAsync(_endpoint);

            _wireMockServer.Reset();
            _wireMockServer
                .Given(_request)
                .RespondWith(Response.Create().WithNotFound());

            var responseApiCache = await _httpClient.GetAsync(_endpoint);

            //Assert
            responseApiCache.Should().Be200Ok().And.BeAs(participantes);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            var memoryCache = _apiFixture.GetService<IMemoryCache>();
            memoryCache.Remove(_systemSettings.FilmesCacheKey);

            _wireMockServer.Dispose();
            _httpClient.Dispose();
        }
    }
}
