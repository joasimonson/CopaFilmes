using System;

namespace CopaFilmes.Api
{
    [Serializable]
    internal class QtdeFilmesDisputaIncorretaException : RegraException
    {
        public QtdeFilmesDisputaIncorretaException() : base("A quantidade de filmes selecionados para o campeonato está incorreta.")
        {
        }
    }
}