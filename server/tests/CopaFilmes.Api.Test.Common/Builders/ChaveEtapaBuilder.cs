using CopaFilmes.Api.Dominio.Campeonato;
using CopaFilmes.Api.Model;
using System.Collections.Generic;

namespace CopaFilmes.Api.Test.Common.Builders
{
    internal class ChaveEtapaBuilder
    {
        private readonly FilmeModelFaker _filmeModelFaker;
        private List<FilmeModel> _participantes;
        private bool _semParticipantes;
        private bool _semChaveamento;

        public ChaveEtapaBuilder()
        {
            _filmeModelFaker = new FilmeModelFaker();
        }

        internal static ChaveEtapaBuilder Novo()
        {
            return new ChaveEtapaBuilder();
        }

        internal ChaveEtapaBuilder ComParticipantes(List<FilmeModel> participantes)
        {
            _participantes = participantes;
            return this;
        }

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

        internal ChaveEtapaBuilder SemChaveamento()
        {
            _semChaveamento = true;
            return this;
        }

        internal ChaveEtapaBuilder SemParticipantes()
        {
            _semParticipantes = true;
            return this;
        }

        internal IEnumerable<FilmeModel> ObterParticipantes()
        {
            return _participantes;
        }

        internal FilmeModel ObterFinalista()
        {
            return _filmeModelFaker.ComTitulo("Vingadores: Guerra Infinita").ComNota(8.8m).Generate();
        }

        internal ChaveEtapa Build()
        {
            if (_participantes is null && !_semParticipantes)
            {
                _participantes = _filmeModelFaker.GenerateDifferentList();
            }

            ChaveEtapa chave = null; // new ChaveEtapa(_participantes);

            if (!_semChaveamento)
            {
                chave.MontarChaveamento();
            }
            
            return chave;
        }
    }
}
