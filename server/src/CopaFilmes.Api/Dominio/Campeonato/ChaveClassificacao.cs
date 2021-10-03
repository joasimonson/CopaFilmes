using CopaFilmes.Api.Model;
using CopaFilmes.Api.Settings;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CopaFilmes.Tests")]

namespace CopaFilmes.Api.Dominio.Campeonato
{
    internal class ChaveClassificacao : ChaveCampeonato
    {
        public ChaveClassificacao(IOptions<SystemSettings> systemSettings, IEnumerable<FilmeModel> participantes)
            : base(systemSettings, participantes)
        {
            if (participantes is null || participantes.Count() != systemSettings.Value.MaximoParticipantesCampeonato)
            {
                throw new QtdeIncorretaRegraChaveamentoException(systemSettings.Value.MaximoParticipantesCampeonato);
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
