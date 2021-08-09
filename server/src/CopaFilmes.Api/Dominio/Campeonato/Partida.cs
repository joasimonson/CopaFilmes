using CopaFilmes.Api.Model;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CopaFilmes.Api.Test")]

namespace CopaFilmes.Api.Dominio.Campeonato
{
    internal class Partida
    {
        private readonly IEnumerable<FilmeModel> _participantes;

        public Partida(FilmeModel desafiante, FilmeModel desafiado)
        {
            _participantes = new List<FilmeModel>
            {
                { desafiante },
                { desafiado }
            };
        }

        public FilmeModel Disputar()
        {
            return ObterParticipantes().First();
        }

        public IEnumerable<FilmeModel> ObterParticipantes()
        {
            return _participantes.OrderByDescending(p => p.Nota).ThenBy(p => p.Titulo);
        }
    }
}
