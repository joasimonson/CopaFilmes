using CopaFilmes.Tests.Common.Builders;
using FluentAssertions;
using Xunit;

namespace CopaFilmes.Tests.Unit.Dominio.Campeonato
{
    public class ChaveClassificacaoTests
    {
        private readonly FilmeModelFaker _filmeModelFaker;

        public ChaveClassificacaoTests()
        {
            _filmeModelFaker = new FilmeModelFaker();
        }

        [Theory]
        [InlineData(8)]
        public void ChaveClassificacao_DeveMontarChaveamentoComQtdeParticipantes(int qtdeParticipantes)
        {
            //Arrange
            var filmes = _filmeModelFaker.Generate(qtdeParticipantes);
            var chave = ChaveClassificacaoBuilder.Novo().ComParticipantes(filmes).SemChaveamento().Build();

            //Act
            var partidas = chave.MontarChaveamento();

            //Assert
            partidas.Should().HaveCount(qtdeParticipantes / 2);
        }

        [Fact]
        public void ChaveClassificacao_DeveDisputarEtapaClassificacao()
        {
            //Arrange
            var chave = ChaveClassificacaoBuilder.Novo().ComChaveClassificacao().Build();
            var finalistas = ChaveEtapaBuilder.Novo().ComChaveEtapa1().ObterParticipantes();

            //Act
            var finalistasEtapa = chave.Disputar();

            //Assert
            finalistasEtapa.Should().BeEquivalentTo(finalistas, opt => opt
                .Including(f => f.Nota)
                .Including(f => f.Titulo)
                .WithStrictOrdering());
        }
    }
}
