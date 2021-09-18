using CopaFilmes.Api.Dominio.Campeonato;
using CopaFilmes.Api.Model;
using System.Collections.Generic;

namespace CopaFilmes.Tests.Common.Builders
{
    internal class ChaveEtapaBuilder : ChaveCampeonatoBuilder<ChaveEtapaBuilder, ChaveEtapa>
    {
        internal ChaveEtapaBuilder ComChaveEtapa1()
        {
            _participantes = new List<FilmeModel>
            {
                _filmeModelFaker.ComTitulo("Vingadores: Guerra Infinita").ComNota(8.8m).Generate(),
                _filmeModelFaker.ComTitulo("Thor: Ragnarok").ComNota(7.9m).Generate(),
                _filmeModelFaker.ComTitulo("Os Incríveis 2").ComNota(8.5m).Generate(),
                _filmeModelFaker.ComTitulo("Jurassic World: Reino Ameaçado").ComNota(6.7m).Generate()
            };
            return this;
        }

        internal ChaveEtapaBuilder ComChaveFinalistas()
        {
            _participantes = new List<FilmeModel>
            {
                _filmeModelFaker.ComTitulo("Vingadores: Guerra Infinita").ComNota(8.8m).Generate(),
                _filmeModelFaker.ComTitulo("Os Incríveis 2").ComNota(8.5m).Generate(),
            };
            return this;
        }

        internal FilmeModel ObterFinalista()
        {
            return _filmeModelFaker.ComTitulo("Vingadores: Guerra Infinita").ComNota(8.8m).Generate();
        }
    }
}
