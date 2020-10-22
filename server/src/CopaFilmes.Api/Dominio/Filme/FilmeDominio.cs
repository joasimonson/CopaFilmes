using CopaFilmes.Api.Externo;
using CopaFilmes.Api.Model;
using Flurl.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Dominio.Filme
{
    public class FilmeDominio : IFilmeDominio
    {
        public async Task<IEnumerable<FilmeModel>> ObterFilmesAsync()
        {
            try
            {
                var filmes = await Parametros.URI_FILMES.GetJsonAsync<List<FilmeModel>>();

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
