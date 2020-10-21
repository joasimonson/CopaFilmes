using CopaFilmes.Api.Model;
using System.Collections.Generic;
using System.Linq;

namespace CopaFilmes.Api.Servicos.Campeonato
{
    public class ChaveClassificacaoPar : ChaveCampeonato
    {
        public ChaveClassificacaoPar(IEnumerable<FilmeModel> filmesCampeonato)
            : base(filmesCampeonato)
        {

        }

        protected override void MontarChaveamento(IEnumerable<FilmeModel> filmesCampeonato)
        {
            int rodadas = _qtdeParticipantes / 2;
            int desafiado = _qtdeParticipantes - 1;

            for (int desafiante = 0; desafiante < rodadas; desafiante++)
            {
                var primeiro = filmesCampeonato.ElementAt(desafiante);
                var segundo = filmesCampeonato.ElementAt(desafiado);

                Partidas.Add(new Partida()
                {
                    Primeiro = primeiro,
                    Segundo = segundo
                });

                desafiado--;
            }
        }
    }
}
