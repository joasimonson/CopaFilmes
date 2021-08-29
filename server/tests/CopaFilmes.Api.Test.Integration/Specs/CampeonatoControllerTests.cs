﻿using AutoBogus;
using CopaFilmes.Api.Servicos.Campeonato;
using CopaFilmes.Api.Settings;
using CopaFilmes.Api.Test.Common.Builders;
using CopaFilmes.Api.Test.Common.Util;
using CopaFilmes.Api.Test.Integration.Fixtures;
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

namespace CopaFilmes.Api.Test.Integration.Specs
{
    [Collection(nameof(ApiTestCollection))]
    public class CampeonatoControllerTests : IDisposable
    {
        private readonly SystemSettings _systemSettings;
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;
        private readonly WireMockServer _wireMockServer;

        public CampeonatoControllerTests(ApiTokenFixture apiTokenFixture)
        {
            _systemSettings = ConfigManager.SystemSettings;
            _endpoint = apiTokenFixture.ConfigRunTests.EndpointCampeonato;
            _httpClient = apiTokenFixture.GetAuthenticatedClient().GetAwaiter().GetResult();

            _wireMockServer = WireMockServer.Start(apiTokenFixture.ConfigRunTests.ServerPort);
        }

        [Fact]
        public async Task Post_DeveRetornarBadRequest_QuandoRequestForInvalido()
        {
            var qtdeParticipantesInvalida = _systemSettings.MaximoParticipantesCampeonato + 1;
            var request = new AutoFaker<CampeonatoRequest>().Generate(qtdeParticipantesInvalida);

            var response = await _httpClient.PostAsync(_endpoint, request.AsHttpContent());

            response.Should().Be400BadRequest();
        }

        [Fact]
        public async Task Post_DeveRetornarInternalServerError_QuandoFalhaAoRetornarListaDeFilmes()
        {
            var participantes = _systemSettings.MaximoParticipantesCampeonato;
            var request = new AutoFaker<CampeonatoRequest>().Generate(participantes);

            _wireMockServer
                .Given(Request
                    .Create()
                    .WithPath(new WildcardMatcher(ConfigManager.ApiFilmesSettings.EndpointFilmes))
                    .UsingGet())
                .RespondWith(Response
                    .Create()
                    .WithNotFound());

            var response = await _httpClient.PostAsync(_endpoint, request.AsHttpContent());

            response.Should().Be500InternalServerError();
        }

        [Fact]
        public async Task Post_DeveRetornarOk_QuandoRequestForValido()
        {
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

            var response = await _httpClient.PostAsync(_endpoint, request.AsHttpContent());

            response.Should().Be200Ok().And.BeAs(finalistas);
        }

        public void Dispose()
        {
            _wireMockServer.Dispose();
        }
    }
}