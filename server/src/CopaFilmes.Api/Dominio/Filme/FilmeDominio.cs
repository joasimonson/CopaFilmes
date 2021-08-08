using CopaFilmes.Api.Model;
using CopaFilmes.Api.Settings;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Dominio.Filme
{
    public class FilmeDominio : IFilmeDominio
    {
        private readonly ApiFilmesSettings _apiFilmesSettings;

        public FilmeDominio(IOptions<ApiFilmesSettings> apiFilmesSettings)
        {
            _apiFilmesSettings = apiFilmesSettings.Value;
        }

        public async Task<IEnumerable<FilmeModel>> ObterFilmesAsync()
        {
            var url = Url.Combine(_apiFilmesSettings.Url, _apiFilmesSettings.EndpointFilmes);
            var filmes = await url.GetJsonAsync<List<FilmeModel>>();
            return filmes;
        }
    }
}
