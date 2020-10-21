using CopaFilmes.Api.Dominio;
using CopaFilmes.Api.Model;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Servicos.Campeonato
{
    public class CampeonatoServico : ICampeonatoServico
    {
        private readonly ICampeonatoParSimplesDominio _campeonatoParSimples;
        private readonly int _qtdeFilmesDisputa;

        public CampeonatoServico(ICampeonatoParSimplesDominio campeonatoParSimples, IConfiguration configuration)
        {
            _campeonatoParSimples = campeonatoParSimples;
            _qtdeFilmesDisputa = configuration.GetValue<int>("QtdeFilmesDisputa");
        }

        public async Task<IEnumerable<FilmePosicaoModel>> Disputar(IEnumerable<CampeonatoRequest> campeonato)
        {
            if (campeonato.Count() != _qtdeFilmesDisputa)
            {
                throw new QtdeFilmesDisputaIncorretaException();
            }

            var idsFilmeCampeonato = campeonato.Select(c => c.IdFilme).ToArray();

            var filmesPosicao = await _campeonatoParSimples.Disputar(idsFilmeCampeonato);

            return filmesPosicao;
        }
    }
}
