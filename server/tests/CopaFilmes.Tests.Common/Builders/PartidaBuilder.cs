using CopaFilmes.Api.Dominio.Campeonato;
using CopaFilmes.Api.Model;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CopaFilmes.Tests.Unit")]
[assembly: InternalsVisibleTo("CopaFilmes.Tests.Integration")]

namespace CopaFilmes.Tests.Common.Builders
{
    internal class PartidaBuilder
    {
        private readonly FilmeModelFaker _filmeModelFaker;
        private FilmeModel _desafiante;
        private FilmeModel _desafiado;

        public PartidaBuilder()
        {
            _filmeModelFaker = FilmeModelFaker.Novo();
        }

        internal static PartidaBuilder Novo()
        {
            return new PartidaBuilder();
        }

        internal PartidaBuilder ComEmpateNota()
        {
            decimal notaEmpate = UtilFaker.Nota();
            _desafiante = _filmeModelFaker.ComNota(notaEmpate).Generate();
            _desafiado = _filmeModelFaker.ComNota(notaEmpate).Generate();
            return this;
        }

        internal PartidaBuilder ComDesafiante(FilmeModel desafiante)
        {
            _desafiante = desafiante;
            return this;
        }

        internal PartidaBuilder ComDesafiado(FilmeModel desafiado)
        {
            _desafiado = desafiado;
            return this;
        }

        internal PartidaBuilder ComParticipantes(IEnumerable<FilmeModel> participantes)
        {
            return ComDesafiante(participantes.First()).ComDesafiado(participantes.Last());
        }

        internal Partida Build()
        {
            if (_desafiante is null)
            {
                _desafiante = _filmeModelFaker.Exclude(_desafiado).Generate();
            }

            if (_desafiado is null)
            {
                _desafiado = _filmeModelFaker.Exclude(_desafiante).Generate();
            }

            return new Partida(_desafiante, _desafiado);
        }
    }
}
