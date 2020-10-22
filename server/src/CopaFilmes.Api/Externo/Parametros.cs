namespace CopaFilmes.Api.Externo
{
    public static class Parametros
    {
        private const string URL_API = "http://copafilmes.azurewebsites.net/api/";
        private const string URN_FILMES = "filmes";
        public static string URI_FILMES => URL_API + URN_FILMES;
        public const int MAX_PARTICIPANTES_CAMPEONATO = 8;
    }
}
