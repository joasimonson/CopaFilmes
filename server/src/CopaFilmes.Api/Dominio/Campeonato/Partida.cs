using CopaFilmes.Api.Model;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CopaFilmes.Tests")]

namespace CopaFilmes.Api.Dominio.Campeonato;

internal class Partida
{
	private readonly IEnumerable<FilmeModel> _participantes;

	public Partida(FilmeModel desafiante, FilmeModel desafiado) => _participantes = new List<FilmeModel>
			{
				{ desafiante },
				{ desafiado }
			};

	public FilmeModel Disputar() => ObterParticipantes().First();

	public IEnumerable<FilmeModel> ObterParticipantes() => _participantes.OrderByDescending(p => p.Nota).ThenBy(p => p.Titulo);
}
