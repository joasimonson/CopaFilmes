using System;

namespace CopaFilmes.Api
{
    [Serializable]
    internal class QtdeIncorretaRegraChaveamentoParException : RegraException
    {
        public QtdeIncorretaRegraChaveamentoParException()
            : base("A quantidade de times é imcompatível com a exigida pela regra de chaveamento Par.")
        {
        }
    }
}