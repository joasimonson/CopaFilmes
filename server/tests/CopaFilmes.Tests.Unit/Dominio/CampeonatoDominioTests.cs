using CopaFilmes.Api;
using CopaFilmes.Api.Dominio;
using CopaFilmes.Api.Dominio.Campeonato;
using CopaFilmes.Api.Model;
using CopaFilmes.Tests.Common.Builders;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CopaFilmes.Tests.Unit.Dominio;

public class CampeonatoDominioTests : BaseTests
{
	private readonly IFilmeDominio _filmeDominioFake;
	private readonly ICampeonatoDominio _campeonatoDominio;
	private readonly IEnumerable<FilmeModel> _participantes;

	public CampeonatoDominioTests()
	{
		_filmeDominioFake = A.Fake<IFilmeDominio>();
		_campeonatoDominio = new CampeonatoDominio(Options.Create(_systemSettings), _filmeDominioFake);

		_participantes = ChaveClassificacaoBuilder.Novo().ComParticipantesFixos().ObterParticipantes();

		A.CallTo(() => _filmeDominioFake.ObterFilmesAsync()).Returns(_participantes);
	}

	[Fact]
	public async Task Disputar_DeveRetornarFinalistas()
	{
		//Arrange
		string[] idsParticipantes = _participantes.Select(p => p.Id).ToArray();

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
		string[] participantesInvalidos = Array.Empty<string>();

		//Act
		Func<Task> act = async () => await _campeonatoDominio.Disputar(participantesInvalidos);

		//Assert
		await act.Should().ThrowAsync<Exception>();
	}
}
