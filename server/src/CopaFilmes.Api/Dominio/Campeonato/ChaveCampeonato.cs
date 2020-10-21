using CopaFilmes.Api.Model;
using System.Collections.Generic;
using System.Linq;

namespace CopaFilmes.Api.Servicos.Campeonato
{
    public abstract class ChaveCampeonato
    {
        protected readonly int _qtdeParticipantes;
        public ICollection<Partida> Partidas { get; protected set; }

        protected ChaveCampeonato(IEnumerable<FilmeModel> filmesCampeonato)
        {
            _qtdeParticipantes = filmesCampeonato.Count();

            Partidas = new List<Partida>();

            MontarChaveamento(filmesCampeonato);
        }

        protected abstract void MontarChaveamento(IEnumerable<FilmeModel> filmesCampeonato);

        public virtual IEnumerable<FilmeModel> Disputar()
        {
            var filmesGanhadores = Partidas.Select(d => d.Disputar());

            return filmesGanhadores;
        }
    }
}
