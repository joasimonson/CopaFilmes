﻿using System;

namespace CopaFilmes.Api
{
    [Serializable]
    internal class QtdeIncorretaRegraChaveamentoException : RegraException
    {
        public QtdeIncorretaRegraChaveamentoException(int qtde)
            : base($"A quantidade de times é incompatível com a exigida pela regra de chaveamento. (Quantidade esperada: {qtde})")
        {
        }
    }
}