using CopaFilmes.Api.Test.Common.Builders;
using FluentAssertions;
using Xunit;

namespace CopaFilmes.Api.Test.Unit.Dominio.Campeonato
{
    public class PartidaTest
    {
        [Theory]
        [InlineData(10, 0)]
        [InlineData(9, 3)]
        [InlineData(6, 5)]
        public void Partida_DesafianteComNotaMaiorDeveSerGanhador(int notaDesafiante, int notaDesafiado)
        {
            //Arrange
            var desafiante = FilmeModelFaker.Novo().ComNota(notaDesafiante).Generate();
            var desafiado = FilmeModelFaker.Novo().ComNota(notaDesafiado).Generate();
            var partida = PartidaBuilder.Novo().ComDesafiante(desafiante).ComDesafiado(desafiado).Build();

            //Act
            var ganhador = partida.Disputar();

            //Assert
            ganhador.Should().BeEquivalentTo(desafiante);
        }

        [Theory]
        [InlineData(0, 10)]
        [InlineData(3, 9)]
        [InlineData(5, 6)]
        public void Partida_DesafiadoComNotaMaiorDeveSerGanhador(int notaDesafiante, int notaDesafiado)
        {
            //Arrange
            var desafiante = FilmeModelFaker.Novo().ComNota(notaDesafiante).Generate();
            var desafiado = FilmeModelFaker.Novo().ComNota(notaDesafiado).Generate();
            var partida = PartidaBuilder.Novo().ComDesafiante(desafiante).ComDesafiado(desafiado).Build();

            //Act
            var ganhador = partida.Disputar();

            //Assert
            ganhador.Should().BeEquivalentTo(desafiado);
        }

        [Theory]
        [InlineData(2, "Amanda", "José")]
        [InlineData(5, "Bianca", "Diego")]
        [InlineData(7, "Marcela", "Marcelo")]
        public void Partida_DeveTerDesempatePorOrdemAlfabeticaDesafiante(int notaEmpate, string nomeDesafiante, string nomeDesafiado)
        {
            //Arrange
            var desafiante = FilmeModelFaker.Novo().ComNota(notaEmpate).ComTitulo(nomeDesafiante).Generate();
            var desafiado = FilmeModelFaker.Novo().ComNota(notaEmpate).ComTitulo(nomeDesafiado).Generate();
            var partida = PartidaBuilder.Novo().ComDesafiante(desafiante).ComDesafiado(desafiado).Build();

            //Act
            var ganhador = partida.Disputar();

            //Assert
            ganhador.Should().BeEquivalentTo(desafiante);
        }

        [Theory]
        [InlineData(7, "José", "Amanda")]
        [InlineData(2, "Diego", "Bianca")]
        [InlineData(5, "Marcelo", "Marcela")]
        public void Partida_DeveTerDesempatePorOrdemAlfabeticaDesafiado(int notaEmpate, string nomeDesafiante, string nomeDesafiado)
        {
            //Arrange
            var desafiante = FilmeModelFaker.Novo().ComNota(notaEmpate).ComTitulo(nomeDesafiante).Generate();
            var desafiado = FilmeModelFaker.Novo().ComNota(notaEmpate).ComTitulo(nomeDesafiado).Generate();
            var partida = PartidaBuilder.Novo().ComDesafiante(desafiante).ComDesafiado(desafiado).Build();

            //Act
            var ganhador = partida.Disputar();

            //Assert
            ganhador.Should().BeEquivalentTo(desafiado);
        }

        [Fact]
        public void Partida_DeveObterParticipantes()
        {
            //Arrange
            var participantes = FilmeModelFaker.Novo().GenerateDifferentList(2);
            var partida = PartidaBuilder.Novo().ComParticipantes(participantes).Build();

            //Act
            var participantesPartida = partida.ObterParticipantes();

            //Assert
            participantesPartida.Should().BeEquivalentTo(participantes);
        }

        [Fact]
        public void Partida_DeveObterParticipantesChaveFinalistaNaOrdemGanhador()
        {
            //Arrange
            var participantes = FilmeModelFaker.Novo().GenerateDifferentList(2);
            var partida = PartidaBuilder.Novo().ComParticipantes(participantes).Build();

            //Act
            var participantesPartida = partida.ObterParticipantes();
            
            //Assert
            participantesPartida.Should().BeEquivalentTo(participantes, opt => opt.WithStrictOrdering());
        }
    }
}
