using CopaFilmes.Api.Model;
using CopaFilmes.Api.Settings;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Dominio.Filme
{
    public class FilmeDominio : IFilmeDominio
    {
        private readonly ApiFilmesSettings _apiFilmesSettings;
        //private readonly IFlurlClient _flurlClient;

        public FilmeDominio(IOptions<ApiFilmesSettings> apiFilmesSettings/*, IFlurlClientFactory flurlClientFactory*/)
        {
            _apiFilmesSettings = apiFilmesSettings.Value;
            //var url = new Url(_apiFilmesSettings.Url);
            //_flurlClient = flurlClientFactory.Get(url);
        }

        public async Task<IEnumerable<FilmeModel>> ObterFilmesAsync()
        {
            //var filmes = await _flurlClient.Request(_apiFilmesSettings.EndpointFilmes).GetJsonAsync<List<FilmeModel>>();
            var filmes = await Url.Combine(_apiFilmesSettings.Url, _apiFilmesSettings.EndpointFilmes).GetJsonAsync<List<FilmeModel>>();
            return filmes;
        }
    }
}
