using CopaFilmes.Api.Settings;
using CopaFilmes.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace CopaFilmes.Api.Dominio.Campeonato
{
    internal class CampeonatoDominio : ICampeonatoDominio
    {
        private readonly IOptions<SystemSettings> _systemSettings;
        private readonly IFilmeDominio _filmeDominio;

        public CampeonatoDominio(IOptions<SystemSettings> systemSettings, IFilmeDominio filmeDominio)
        {
            _systemSettings = systemSettings;
            _filmeDominio = filmeDominio;
        }

        public async Task<IEnumerable<FilmePosicaoModel>> Disputar(string[] idsParticipantes)
        {
            if (idsParticipantes is null || idsParticipantes.Length != _systemSettings.Value.MaximoParticipantesCampeonato)
            {
                throw new QtdeIncorretaRegraChaveamentoException(_systemSettings.Value.MaximoParticipantesCampeonato);
            }

            var filmes = await _filmeDominio.ObterFilmesAsync();

            var filmesCampeonato = filmes.Where(f => idsParticipantes.Contains(f.Id)).OrderBy(f => f.Titulo);

            var filmesPosicao = DisputarCampeonato(filmesCampeonato);

            return filmesPosicao;
        }

        private IEnumerable<FilmePosicaoModel> DisputarCampeonato(IEnumerable<FilmeModel> filmesCampeonato)
        {
            var chaveClassificacao = new ChaveClassificacao(_systemSettings, filmesCampeonato);

            chaveClassificacao.MontarChaveamento();

            var chaveFinal = DisputarRodadas(chaveClassificacao);

            var finalistas = chaveFinal.ObterParticipantesPosicao();

            return finalistas;
        }

        private ChaveCampeonato DisputarRodadas(ChaveCampeonato chaveDisputa)
        {
            var filmesGanhadores = chaveDisputa.Disputar();

            var chaveEtapa = new ChaveEtapa(_systemSettings, filmesGanhadores);

            chaveEtapa.MontarChaveamento();

            if (chaveEtapa.ChaveFinalistas)
            {
                return chaveEtapa;
            }

            return DisputarRodadas(chaveEtapa);
        }
    }
}
