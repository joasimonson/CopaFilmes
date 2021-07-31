using CopaFilmes.Api.Dominio;
using CopaFilmes.Api.Dominio.Campeonato;
using CopaFilmes.Api.Model;
using CopaFilmes.Api.Settings;
using CopaFilmes.Api.Test.Common.Builders;
using CopaFilmes.Api.Test.Common.Util;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CopaFilmes.Api.Test.Unit.Dominio
{
    public class CampeonatoDominioTest
    {
        private readonly IFilmeDominio _filmeDominio;
        private readonly ICampeonatoDominio _campeonatoDominio;
        private readonly SystemSettings _systemSettings;
        private readonly IEnumerable<FilmeModel> _participantes;

        public CampeonatoDominioTest()
        {
            _systemSettings = ConfigManager.SystemSettings;
            _filmeDominio = A.Fake<IFilmeDominio>();
            _campeonatoDominio = new CampeonatoDominio(Options.Create(_systemSettings), _filmeDominio);

            _participantes = ChaveClassificacaoBuilder.Novo().ComParticipantesFixos().ObterParticipantes();

            A.CallTo(() => _filmeDominio.ObterFilmesAsync()).Returns(_participantes);
        }

        [Fact]
        public async Task Disputar_DeveRetornarFinalistas()
        {
            //Arrange
            var idsParticipantes = _participantes.Select(p => p.Id).ToArray();

            //Act
            var finalistasDisputa = await _campeonatoDominio.Disputar(idsParticipantes);

            //Assert
            finalistasDisputa.Should().BeEquivalentTo(finalistasDisputa);
        }

        [Fact]
        public async Task Disputar_DeveGerarFalhaAoGerarChaveComParticipantesInvalidos()
        {
            //Arrange
            string[] participantesInvalidos = null;

            //Act
            Func<Task> act = async () => await _campeonatoDominio.Disputar(participantesInvalidos);

            //Assert
            await act.Should().ThrowAsync<QtdeIncorretaRegraChaveamentoException>();
        }

        [Fact]
        public async Task Disputar_DeveGerarFalhaAoGerarChaveComListaVaziaDeParticipantes()
        {
            //Arrange
            var participantesInvalidos = Array.Empty<string>();

            //Act
            Func<Task> act = async () => await _campeonatoDominio.Disputar(participantesInvalidos);
            
            //Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
