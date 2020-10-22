using CopaFilmes.Api.Dominio.Campeonato;
using CopaFilmes.Api.Test.Builders;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace CopaFilmes.Api.Test.Dominio.Campeonato
{
    public class ChaveEtapaTest
    {
        public readonly FilmeModelFaker _filmeModelFaker;

        public ChaveEtapaTest()
        {
            _filmeModelFaker = new FilmeModelFaker();
        }

        [Theory]
        [InlineData(4)]
        [InlineData(8)]
        public void ChaveEtapa_DeveMontarChaveamentoComParticipantes(int qtdeParticipantes)
        {
            //Arrange
            var participantes = _filmeModelFaker.Generate(qtdeParticipantes);
            var chave = new ChaveEtapa(participantes);

            //Act
            var partidas = chave.MontarChaveamento();

            //Assert
            partidas.Should().HaveCount(qtdeParticipantes / 2);
        }

        [Fact]
        public void ChaveEtapa_DeveDisputarEtapaComum()
        {
            //Arrange
            var chave = ChaveEtapaBuilder.Novo().ComChaveEtapa1().Build();
            var finalistas = ChaveEtapaBuilder.Novo().ComChaveFinalistas().ObterParticipantes();

            //Act
            var finalistasEtapa = chave.Disputar();

            //Assert
            finalistasEtapa.Should().BeEquivalentTo(finalistas, opt => opt
                .Including(f => f.Nota)
                .Including(f => f.Titulo)
                .WithStrictOrdering());
        }

        [Fact]
        public void ChaveEtapa_DeveDisputarEtapaFinal()
        {
            //Arrange
            var chave = ChaveEtapaBuilder.Novo().ComChaveFinalistas().Build();
            var finalista = ChaveEtapaBuilder.Novo().ObterFinalista();

            //Act
            var finalistaEtapa = chave.Disputar().First();

            //Assert
            finalistaEtapa.Should().BeEquivalentTo(finalista, opt => opt
                .Including(f => f.Nota)
                .Including(f => f.Titulo));
        }
    }
}
