using CopaFilmes.Api.Model;
using CopaFilmes.Api.Settings;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CopaFilmes.Tests.Unit")]
[assembly: InternalsVisibleTo("CopaFilmes.Tests.Common")]
[assembly: InternalsVisibleTo("CopaFilmes.Tests.Integration")]

namespace CopaFilmes.Api.Dominio.Campeonato
{
    internal class ChaveEtapa : ChaveCampeonato
    {
        public ChaveEtapa(IOptions<SystemSettings> systemSettings, IEnumerable<FilmeModel> participantes)
            : base(systemSettings, participantes)
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
