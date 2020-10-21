using CopaFilmes.Api.Model;
using CopaFilmes.Api.Externo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Dominio.Filme
{
    public class FilmeDominio : IFilmeDominio
    {
        private readonly Endpoints _endpoints;
        private readonly IRecursos _recursos;

        public FilmeDominio(Endpoints endpoints, IRecursos recursos)
        {
            _endpoints = endpoints;
            _recursos = recursos;
        }

        public async Task<IEnumerable<FilmeModel>> ObterFilmesAsync()
        {
            var filmes = await _recursos.GetAsync<IEnumerable<FilmeModel>>(_endpoints.URI_FILMES);

            return filmes;
        }
    }
}
