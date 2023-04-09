using CopaFilmes.Api;
using CopaFilmes.Api.Model;
using CopaFilmes.Tests.Common.Builders;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace CopaFilmes.Tests.Unit.Dominio.Campeonato;

public class ChaveCampeonatoTests
{
	[Fact]
	public void ChaveCampeonato_DeveGerarFalhaComNumeroParticipantesIncorreto()
	{
		//Arrange
		int qtdeParticipantesIncorreta = UtilFaker.FakerHub.Random.Odd(1, 99);

		//Act
		Action act = () => ChaveClassificacaoBuilder.Novo().ComParticipantes(qtdeParticipantesIncorreta).Build();

		//Assert
		act.Should().Throw<Exception>().WithInnerException<QtdeIncorretaRegraChaveamentoException>();
	}

	[Fact]
	public void ChaveCampeonato_DeveGerarFalhaAoGerarChaveComParticipantesNulo()
	{
		//Arrange
		List<FilmeModel> participantesInvalidos = null;

		//Act
		Action act = () => ChaveClassificacaoBuilder.Novo().ComParticipantes(participantesInvalidos).Build();

		//Assert
		act.Should().Throw<Exception>().WithInnerException<QtdeIncorretaRegraChaveamentoException>();
	}

	[Fact]
	public void ChaveCampeonato_DeveGerarFalhaAoGerarChaveComListaVaziaDeParticipantes()
	{
		//Arrange
		var participantesInvalidos = new List<FilmeModel>();

		//Act
		Action act = () => ChaveClassificacaoBuilder.Novo().ComParticipantes(participantesInvalidos).Build();

		//Assert
		act.Should().Throw<Exception>().WithInnerException<QtdeIncorretaRegraChaveamentoException>();
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
