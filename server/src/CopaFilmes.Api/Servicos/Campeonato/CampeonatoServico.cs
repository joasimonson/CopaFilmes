using CopaFilmes.Api.Dominio;
using CopaFilmes.Api.Model;
using CopaFilmes.Api.Settings;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Servicos.Campeonato
{
    public class CampeonatoServico : ICampeonatoServico
    {
        private readonly SystemSettings _systemSettings;
        private readonly ICampeonatoDominio _campeonatoParSimples;

        public CampeonatoServico(IOptions<SystemSettings> systemSettings, ICampeonatoDominio campeonatoParSimples)
        {
            _systemSettings = systemSettings.Value;
            _campeonatoParSimples = campeonatoParSimples;
        }

        public async Task<IEnumerable<FilmePosicaoModel>> Disputar(IEnumerable<CampeonatoRequest> campeonato)
        {
            if (campeonato is null || campeonato.Count() != _systemSettings.MaximoParticipantesCampeonato)
            {
                throw new QtdeFilmesDisputaIncorretaException();
            }

            var idsFilmeCampeonato = campeonato.Select(c => c.IdFilme).ToArray();

            var filmesPosicao = await _campeonatoParSimples.Disputar(idsFilmeCampeonato);

            return filmesPosicao;
        }
    }
}
