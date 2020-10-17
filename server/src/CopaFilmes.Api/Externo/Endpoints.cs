using Microsoft.Extensions.Configuration;

namespace CopaFilmes.Api.Externo
{
    public sealed class Endpoints
    {
        private readonly string URL_API;

        private const string URN_FILMES = "filmes";

        public Endpoints(IConfiguration config)
        {
            string parametroUrlApi = "UrlApi";
            URL_API = config.GetValue<string>(parametroUrlApi);
        }

        public string URI_FILMES => URL_API + URN_FILMES;
    }
}
