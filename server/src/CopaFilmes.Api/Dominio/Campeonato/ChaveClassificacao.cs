using CopaFilmes.Api.Externo;
using CopaFilmes.Api.Model;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CopaFilmes.Api.Test")]

namespace CopaFilmes.Api.Dominio.Campeonato
{
    internal class ChaveClassificacao : ChaveCampeonato
    {
        public ChaveClassificacao(IEnumerable<FilmeModel> participantes)
            : base(participantes)
        {
            if (participantes is null || participantes.Count() != Parametros.MAX_PARTICIPANTES_CAMPEONATO)
            {
                throw new QtdeIncorretaRegraChaveamentoException(Parametros.MAX_PARTICIPANTES_CAMPEONATO);
            }
        }

        public override IEnumerable<Partida> MontarChaveamento()
        {
            IEnumerable<FilmeModel> participantesOrdenados = _participantes.OrderBy(f => f.Titulo);

            int rodadas = _qtdeParticipantes / 2;
            int desafiadoAtual = _qtdeParticipantes - 1;

            for (int desafianteAtual = 0; desafianteAtual < rodadas; desafianteAtual++)
            {
                var desafiante = participantesOrdenados.ElementAt(desafianteAtual);
                var desafiado = participantesOrdenados.ElementAt(desafiadoAtual);

                AdicionarPartida(desafiante, desafiado);

                desafiadoAtual--;
            }

            return _partidas;
        }
    }
}
