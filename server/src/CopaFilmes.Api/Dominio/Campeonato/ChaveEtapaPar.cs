using CopaFilmes.Api.Model;
using System.Collections.Generic;
using System.Linq;

namespace CopaFilmes.Api.Servicos.Campeonato
{
    public class ChaveEtapaPar : ChaveCampeonato
    {
        public ChaveEtapaPar(IEnumerable<Model.FilmeModel> filmesCampeonato)
            : base(filmesCampeonato)
        {

        }

        protected override void MontarChaveamento(IEnumerable<Model.FilmeModel> filmesCampeonato)
        {
            for (int desafiador = 0; desafiador < _qtdeParticipantes; desafiador += 2)
            {
                int desafiado = desafiador + 1;

                var primeito = filmesCampeonato.ElementAt(desafiador);
                var segundo = filmesCampeonato.ElementAt(desafiado);

                Partidas.Add(new Partida()
                {
                    Primeiro = primeito,
                    Segundo = segundo
                });
            }
        }
    }
}
