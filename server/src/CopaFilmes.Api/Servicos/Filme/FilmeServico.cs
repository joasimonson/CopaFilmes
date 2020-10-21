using CopaFilmes.Api.Dominio;
using CopaFilmes.Api.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Servicos.Filme
{
    internal class FilmeServico : IFilmeServico
    {
        private readonly IFilmeDominio _filmeDominio;

        public FilmeServico(IFilmeDominio filmeDominio)
        {
            _filmeDominio = filmeDominio;
        }

        public async Task<IEnumerable<FilmeModel>> ObterFilmesAsync()
        {
            return await _filmeDominio.ObterFilmesAsync();
        }
    }
}
