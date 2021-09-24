using AutoBogus;
using CopaFilmes.Api.Servicos.Campeonato;
using CopaFilmes.Api.Settings;
using CopaFilmes.Tests.Common.Builders;
using CopaFilmes.Tests.Common.Util;
using CopaFilmes.Tests.Integration.Fixtures;
using FluentAssertions;
using System;
using System.Linq;
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
    public class CampeonatoControllerTests : IDisposable
    {
        private readonly SystemSettings _systemSettings;
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;
        private readonly WireMockServer _wireMockServer;

        public CampeonatoControllerTests(ApiFixture apiFixture)
        {
            apiFixture.Initializar().GetAwaiter().GetResult();

            _systemSettings = ConfigManager.SystemSettings;
            _endpoint = apiFixture.ConfigRunTests.EndpointCampeonato;
            _httpClient = apiFixture.GetAuthHttpClient();

            _wireMockServer = WireMockServer.Start(apiFixture.ConfigRunTests.ServerPort);
        }

        [Fact]
        public async Task Post_DeveRetornarBadRequest_QuandoRequestForInvalido()
        {
            //Arrange
            var qtdeParticipantesInvalida = _systemSettings.MaximoParticipantesCampeonato + 1;
            var request = new AutoFaker<CampeonatoRequest>().Generate(qtdeParticipantesInvalida);

            //Act
            var response = await _httpClient.PostAsync(_endpoint, request.AsHttpContent());

            //Assert
            response.Should().Be400BadRequest();
        }

        [Fact]
        public async Task Post_DeveRetornarOk_QuandoRequestForValido()
        {
            //Arrange
            var chaveCampeonato = ChaveClassificacaoBuilder.Novo().ComParticipantesFixos().Build();
            var participantes = chaveCampeonato.ObterParticipantes();
            var chaveFinalistas = ChaveEtapaBuilder.Novo().ComChaveFinalistas().Build();
            var finalistas = chaveFinalistas.ObterParticipantes().Select(f => new { f.Titulo, f.Nota }).ToArray();
            var request = participantes.Select(p => new CampeonatoRequest { IdFilme = p.Id }).ToArray();

            _wireMockServer
                .Given(Request
                    .Create()
                    .WithPath(new WildcardMatcher(ConfigManager.ApiFilmesSettings.EndpointFilmes))
                    .UsingGet())
                .RespondWith(Response
                    .Create()
                    .WithSuccess()
                    .WithBodyAsJson(participantes));

            //Act
            var response = await _httpClient.PostAsync(_endpoint, request.AsHttpContent());

            //Assert
            response.Should().Be200Ok().And.BeAs(finalistas);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _wireMockServer.Dispose();
            _httpClient.Dispose();
        }
    }
}
