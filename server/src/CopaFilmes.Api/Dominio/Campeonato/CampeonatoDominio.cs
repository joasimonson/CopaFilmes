using CopaFilmes.Api.Extensions;
using CopaFilmes.Api.Externo;
using CopaFilmes.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Dominio.Campeonato
{
    internal class CampeonatoDominio : ICampeonatoDominio
    {
        private readonly IFilmeDominio _filmeDominio;

        public CampeonatoDominio(IFilmeDominio filmeDominio)
        {
            _filmeDominio = filmeDominio;
        }

        public async Task<IEnumerable<FilmePosicaoModel>> Disputar(string[] idsParticipantes)
        {
            if (idsParticipantes is null || idsParticipantes.Length != Parametros.MAX_PARTICIPANTES_CAMPEONATO)
            {
                throw new QtdeIncorretaRegraChaveamentoException(Parametros.MAX_PARTICIPANTES_CAMPEONATO);
            }

            var filmes = await _filmeDominio.ObterFilmesAsync();

            var filmesCampeonato = filmes.Where(f => idsParticipantes.Contains(f.Id)).OrderBy(f => f.Titulo);

            var filmesPosicao = DisputarCampeonato(filmesCampeonato);

            return filmesPosicao;
        }

        private IEnumerable<FilmePosicaoModel> DisputarCampeonato(IEnumerable<FilmeModel> filmesCampeonato)
        {
            var chaveClassificacao = new ChaveClassificacao(filmesCampeonato);

            chaveClassificacao.MontarChaveamento();

            var chaveFinal = DisputarRodadas(chaveClassificacao);

            var finalistas = chaveFinal.ObterParticipantesPosicao();

            return finalistas;
        }

        private ChaveCampeonato DisputarRodadas(ChaveCampeonato chaveDisputa)
        {
            var filmesGanhadores = chaveDisputa.Disputar();

            var chaveEtapa = new ChaveEtapa(filmesGanhadores);

            if (chaveEtapa.ChaveFinalistas)
            {
                return chaveEtapa;
            }

            return DisputarRodadas(chaveEtapa);
        }
    }
}
