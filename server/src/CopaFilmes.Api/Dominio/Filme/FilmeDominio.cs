using CopaFilmes.Api.Model;
using CopaFilmes.Api.Settings;
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
            try
            {
                var filmes = await _apiFilmesSettings.URL_FILMES.GetJsonAsync<List<FilmeModel>>();

                return filmes;
            }
            catch (FlurlHttpTimeoutException)
            {
                throw;
            }
            catch (FlurlHttpException)
            {
                throw;
            }
        }
    }
}
