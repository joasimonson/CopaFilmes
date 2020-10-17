using CopaFilmes.Api.Entidades;
using CopaFilmes.Api.Externo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Repositorios
{
    public class FilmeRepositorio : IFilmeRepositorio
    {
        private readonly Endpoints _endpoints;
        private readonly IRecursos _recursos;

        public FilmeRepositorio(Endpoints endpoints, IRecursos recursos)
        {
            _endpoints = endpoints;
            _recursos = recursos;
        }

        public async Task<IEnumerable<Filme>> ObterFilmesAsync()
        {
            var filmes = await _recursos.GetAsync<IEnumerable<Filme>>(_endpoints.URI_FILMES);

            return filmes;
        }
    }
}
