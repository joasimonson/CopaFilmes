using CopaFilmes.Api.Model;
using System.Linq;

namespace CopaFilmes.Api.Servicos.Campeonato
{
    public class Partida
    {
        public FilmeModel Primeiro;
        public FilmeModel Segundo;

        public FilmeModel Disputar()
        {
            FilmeModel[] participantes = new FilmeModel[]
            {
                Primeiro,
                Segundo
            };

            return participantes.OrderByDescending(p => p.Nota).ThenBy(p => p.Titulo).First();
        }
    }
}
