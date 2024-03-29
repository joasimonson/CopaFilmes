﻿using CopaFilmes.Api.Dominio.Campeonato;
using CopaFilmes.Api.Model;
using CopaFilmes.Tests.Common.Util;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CopaFilmes.Tests.Unit")]
[assembly: InternalsVisibleTo("CopaFilmes.Tests.Integration")]

namespace CopaFilmes.Tests.Common.Builders;

internal abstract class ChaveCampeonatoBuilder<TBuilder, TChave> where TChave : ChaveCampeonato
{
	protected readonly FilmeModelFaker _filmeModelFaker;
	protected IEnumerable<FilmeModel> _participantes;
	protected bool _semParticipantes;
	protected bool _semChaveamento;

	protected ChaveCampeonatoBuilder() => _filmeModelFaker = new FilmeModelFaker();

	internal static TBuilder Novo() => Activator.CreateInstance<TBuilder>();

	internal ChaveCampeonatoBuilder<TBuilder, TChave> ComParticipantes(int participantes)
	{
		_participantes = _filmeModelFaker.Generate(participantes);
		return this;
	}

	internal ChaveCampeonatoBuilder<TBuilder, TChave> ComParticipantes(IEnumerable<FilmeModel> participantes)
	{
		_participantes = participantes;
		_semParticipantes = participantes is null;
		return this;
	}

	internal ChaveCampeonatoBuilder<TBuilder, TChave> SemChaveamento()
	{
		_semChaveamento = true;
		return this;
	}

	internal ChaveCampeonatoBuilder<TBuilder, TChave> SemParticipantes()
	{
		_semParticipantes = true;
		return this;
	}

	internal IEnumerable<FilmeModel> ObterParticipantes() => _participantes;

	internal TChave Build()
	{
		if (_participantes is null && !_semParticipantes)
		{
			_participantes = _filmeModelFaker.GenerateDifferentList();
		}

		var chave = Activator.CreateInstance(typeof(TChave), Options.Create(ConfigManager.SystemSettings), _participantes) as TChave;

		if (!_semChaveamento)
		{
			chave.MontarChaveamento();
		}

		return chave;
	}
}
