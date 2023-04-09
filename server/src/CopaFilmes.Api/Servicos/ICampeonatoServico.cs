using CopaFilmes.Api.Model;
using CopaFilmes.Api.Servicos.Campeonato;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Servicos;

public interface ICampeonatoServico
{
	Task<IEnumerable<FilmePosicaoModel>> Disputar(IEnumerable<CampeonatoRequest> campeonato);
}
