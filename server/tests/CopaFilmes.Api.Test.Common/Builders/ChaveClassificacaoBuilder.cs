using CopaFilmes.Api.Dominio.Campeonato;
using CopaFilmes.Api.Model;
using System.Collections.Generic;

namespace CopaFilmes.Api.Test.Common.Builders
{
    internal class ChaveClassificacaoBuilder : ChaveCampeonatoBuilder<ChaveClassificacaoBuilder, ChaveClassificacao>
    {
        internal ChaveClassificacaoBuilder ComParticipantesFixos()
        {
            _participantes = new List<FilmeModel>
            {
                _filmeModelFaker.ComTitulo("Os Incríveis 2").ComNota(8.5m).Generate(),
                _filmeModelFaker.ComTitulo("Jurassic World: Reino Ameaçado").ComNota(6.7m).Generate(),
                _filmeModelFaker.ComTitulo("Oito Mulheres e um Segredo").ComNota(6.3m).Generate(),
                _filmeModelFaker.ComTitulo("Hereditário").ComNota(7.8m).Generate(),
                _filmeModelFaker.ComTitulo("Vingadores: Guerra Infinita").ComNota(8.8m).Generate(),
                _filmeModelFaker.ComTitulo("Deadpool 2").ComNota(8.1m).Generate(),
                _filmeModelFaker.ComTitulo("Han Solo: Uma História Star Wars").ComNota(7.2m).Generate(),
                _filmeModelFaker.ComTitulo("Thor: Ragnarok").ComNota(7.9m).Generate(),
                
            };
            return this;
        }

        internal ChaveClassificacaoBuilder ComChaveClassificacao()
        {
            _participantes = new List<FilmeModel>
            {
                _filmeModelFaker.ComTitulo("Deadpool 2").ComNota(8.1m).Generate(),
                _filmeModelFaker.ComTitulo("Han Solo: Uma História Star Wars").ComNota(7.2m).Generate(),
                _filmeModelFaker.ComTitulo("Hereditário").ComNota(7.8m).Generate(),
                _filmeModelFaker.ComTitulo("Jurassic World: Reino Ameaçado").ComNota(6.7m).Generate(),
                _filmeModelFaker.ComTitulo("Oito Mulheres e um Segredo").ComNota(6.3m).Generate(),
                _filmeModelFaker.ComTitulo("Os Incríveis 2").ComNota(8.5m).Generate(),
                _filmeModelFaker.ComTitulo("Thor: Ragnarok").ComNota(7.9m).Generate(),
                _filmeModelFaker.ComTitulo("Vingadores: Guerra Infinita").ComNota(8.8m).Generate(),
            };
            return this;
        }
    }
}
