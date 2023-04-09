using CopaFilmes.Api.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Servicos;

public interface IFilmeServico
{
	public Task<IEnumerable<FilmeModel>> ObterFilmesAsync();
}
