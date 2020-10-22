using CopaFilmes.Api.Model;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CopaFilmes.Api.Test")]

namespace CopaFilmes.Api.Dominio.Campeonato
{
    internal class ChaveEtapa : ChaveCampeonato
    {
        public ChaveEtapa(IEnumerable<FilmeModel> participantes)
            : base(participantes)
        {

        }

        public override IEnumerable<Partida> MontarChaveamento()
        {
            for (int desafianteAtual = 0; desafianteAtual < _qtdeParticipantes; desafianteAtual += 2)
            {
                int desafiadoAtual = desafianteAtual + 1;

                var desafiante = _participantes.ElementAt(desafianteAtual);
                var desafiado = _participantes.ElementAt(desafiadoAtual);

                AdicionarPartida(desafiante, desafiado);
            }

            return _partidas;
        }
    }
}
