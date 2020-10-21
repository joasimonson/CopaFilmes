using CopaFilmes.Api.Model;
using CopaFilmes.Api.Servicos.Campeonato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Dominio.Campeonato
{
    internal class CampeonatoParSimplesDominio : ICampeonatoParSimplesDominio
    {
        private readonly IFilmeDominio _filmeDominio;

        public CampeonatoParSimplesDominio(IFilmeDominio filmeDominio)
        {
            _filmeDominio = filmeDominio;
        }

        public async Task<IEnumerable<FilmePosicaoModel>> Disputar(string[] idsParticipantes)
        {
            if (idsParticipantes.Length % 2 != 0)
            {
                throw new QtdeIncorretaRegraChaveamentoParException();
            }

            var filmes = await _filmeDominio.ObterFilmesAsync();

            var filmesCampeonato = filmes.Where(f => idsParticipantes.Contains(f.Id)).OrderBy(f => f.Titulo);

            var filmesPosicao = DisputarCampeonato(filmesCampeonato);

            return filmesPosicao;
        }

        private IEnumerable<FilmePosicaoModel> DisputarCampeonato(IEnumerable<FilmeModel> filmesCampeonato)
        {
            var chaveClassificacao = new ChaveClassificacaoPar(filmesCampeonato);

            var finalistas = DisputarRodadas(chaveClassificacao);

            IEnumerable<FilmePosicaoModel> filmesPosicao = IdentificarFinalistas(finalistas);

            return filmesPosicao;
        }

        private IEnumerable<FilmeModel> DisputarRodadas(ChaveCampeonato chaveDisputa)
        {
            var filmesGanhadores = chaveDisputa.Disputar();

            if (filmesGanhadores.Count() == 2)
            {
                return filmesGanhadores;
            }

            var chaveEtapaPar = new ChaveEtapaPar(filmesGanhadores);

            return DisputarRodadas(chaveEtapaPar);
        }

        private IEnumerable<FilmePosicaoModel> IdentificarFinalistas(IEnumerable<FilmeModel> finalistas)
        {
            var filmesPosicao = finalistas
                                    .OrderByDescending(p => p.Nota)
                                    .ThenBy(p => p.Titulo)
                                    .Select((f, index) => new FilmePosicaoModel()
                                    {
                                        Posicao = index + 1,
                                        Id = f.Id,
                                        Titulo = f.Titulo,
                                        Nota = f.Nota,
                                        Ano = f.Ano
                                    });

            return filmesPosicao;
        }
    }
}
