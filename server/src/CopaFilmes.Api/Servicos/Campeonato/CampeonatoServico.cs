using CopaFilmes.Api.Dominio;
using CopaFilmes.Api.Externo;
using CopaFilmes.Api.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Servicos.Campeonato
{
    public class CampeonatoServico : ICampeonatoServico
    {
        private readonly ICampeonatoDominio _campeonatoParSimples;

        public CampeonatoServico(ICampeonatoDominio campeonatoParSimples)
        {
            _campeonatoParSimples = campeonatoParSimples;
        }

        public async Task<IEnumerable<FilmePosicaoModel>> Disputar(IEnumerable<CampeonatoRequest> campeonato)
        {
            if (campeonato is null || campeonato.Count() != Parametros.MAX_PARTICIPANTES_CAMPEONATO)
            {
                throw new QtdeFilmesDisputaIncorretaException();
            }

            var idsFilmeCampeonato = campeonato.Select(c => c.IdFilme).ToArray();

            var filmesPosicao = await _campeonatoParSimples.Disputar(idsFilmeCampeonato);

            return filmesPosicao;
        }
    }
}
