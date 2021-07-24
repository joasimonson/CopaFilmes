using CopaFilmes.Api.Dominio;
using CopaFilmes.Api.Dominio.Campeonato;
using CopaFilmes.Api.Model;
using CopaFilmes.Api.Test.Builders;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CopaFilmes.Api.Test.Dominio
{
    public class CampeonatoDominioTest
    {
        private readonly IEnumerable<FilmeModel> _participantes;
        private readonly IFilmeDominio _filmeDominio;
        private readonly ICampeonatoDominio _campeonatoDominio;

        public CampeonatoDominioTest()
        {
            _filmeDominio = A.Fake<IFilmeDominio>();
            _campeonatoDominio = new CampeonatoDominio(_filmeDominio);

            _participantes = ChaveClassificacaoBuilder.Novo().ComParticipantesFixos().ObterParticipantes();

            A.CallTo(() => _filmeDominio.ObterFilmesAsync()).Returns(_participantes);
        }

        [Fact]
        public async Task Disputar_DeveRetornarFinalistas()
        {
            //Arrange
            var finalistas = ChaveEtapaBuilder.Novo().ComChaveFinalistas().ObterParticipantes();
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
            await act.Should().ThrowAsync<QtdeIncorretaRegraChaveamentoException>();
        }
    }
}
