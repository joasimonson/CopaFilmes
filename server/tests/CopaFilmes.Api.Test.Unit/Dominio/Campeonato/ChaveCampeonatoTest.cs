using CopaFilmes.Api.Dominio.Campeonato;
using CopaFilmes.Api.Model;
using CopaFilmes.Api.Test.Common.Builders;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace CopaFilmes.Api.Test.Unit.Dominio.Campeonato
{
    public class ChaveCampeonatoTest
    {
        private readonly FilmeModelFaker _filmeModelFaker;

        public ChaveCampeonatoTest()
        {
            _filmeModelFaker = new FilmeModelFaker();
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        public void ChaveCampeonato_DeveGerarFalhaComNumeroParticipantesIncorreto(int qtdeParticipantesIncorreta)
        {
            //Arrange
            var filmes = _filmeModelFaker.Generate(qtdeParticipantesIncorreta);

            //Act
            //Action act = () => { var chave = new ChaveClassificacao(filmes); };

            //Assert
            //act.Should().Throw<QtdeIncorretaRegraChaveamentoException>();
        }

        [Fact]
        public void ChaveCampeonato_DeveGerarFalhaAoGerarChaveComParticipantesNulo()
        {
            //Arrange
            IEnumerable<FilmeModel> participantesInvalidos = null;

            //Act
            //Action act = () => { var chave = new ChaveClassificacao(participantesInvalidos); };

            //Assert
            //act.Should().Throw<QtdeIncorretaRegraChaveamentoException>();
        }

        [Fact]
        public void ChaveCampeonato_DeveGerarFalhaAoGerarChaveComListaVaziaDeParticipantes()
        {
            //Arrange
            var participantesInvalidos = new List<FilmeModel>();

            //Act
            //Action act = () => { var chave = new ChaveClassificacao(participantesInvalidos); };

            //Assert
            //act.Should().Throw<QtdeIncorretaRegraChaveamentoException>();
        }

        [Fact]
        public void ChaveCampeonato_DeveGerarFalhaAoObterParticipantesPosicaoSemChaveamento()
        {
            //Arrange
            var chave = ChaveEtapaBuilder.Novo().SemChaveamento().Build();

            //Act
            Action act = () => chave.ObterParticipantesPosicao();

            //Assert
            act.Should().Throw<ChaveNaoMontadaException>();
        }

        [Fact]
        public void ChaveCampeonato_DeveObterParticipantesPosicao()
        {
            //Arrange
            var participantes = ChaveEtapaBuilder.Novo().ComChaveFinalistas().ObterParticipantes();
            var chave = ChaveEtapaBuilder.Novo().ComChaveFinalistas().Build();

            //Act
            var participantesEtapa = chave.ObterParticipantesPosicao();

            //Assert
            participantesEtapa.Should().BeEquivalentTo(participantes, opt => opt
                .Including(f => f.Nota)
                .Including(f => f.Titulo));
        }

        [Fact]
        public void ChaveCampeonato_DeveObterParticipantesPosicaoNaOrdemGanhador()
        {
            //Arrange
            var participantes = ChaveEtapaBuilder.Novo().ComChaveFinalistas().ObterParticipantes();
            var chave = ChaveEtapaBuilder.Novo().ComChaveFinalistas().Build();

            //Act
            var participantesEtapa = chave.ObterParticipantesPosicao();

            //Assert
            participantesEtapa.Should()
                .BeEquivalentTo(participantes, opt => opt
                    .Including(f => f.Nota)
                    .Including(f => f.Titulo))
                .And.BeInAscendingOrder(f => f.Posicao);
        }
    }
}
