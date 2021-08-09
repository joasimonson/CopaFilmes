using CopaFilmes.Api.Extensions;
using CopaFilmes.Api.Model;
using CopaFilmes.Api.Settings;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace CopaFilmes.Api.Dominio.Campeonato
{
    internal abstract class ChaveCampeonato
    {
        protected readonly int _qtdeParticipantes;
        protected readonly IEnumerable<FilmeModel> _participantes;
        protected readonly ICollection<Partida> _partidas;
        public readonly bool ChaveFinalistas;

        protected ChaveCampeonato(IOptions<SystemSettings> systemSettings, IEnumerable<FilmeModel> participantes)
        {
            if (participantes is null || participantes.Count() == 0 || !participantes.Count().EhPar())
            {
                throw new QtdeIncorretaRegraChaveamentoException(systemSettings.Value.MaximoParticipantesCampeonato);
            }

            _qtdeParticipantes = participantes.Count();
            _participantes = participantes;
            _partidas = new List<Partida>();

            ChaveFinalistas = _qtdeParticipantes == 2;
        }

        public abstract IEnumerable<Partida> MontarChaveamento();

        protected virtual void AdicionarPartida(FilmeModel desafiante, FilmeModel desafiado)
        {
            _partidas.Add(new Partida(desafiante, desafiado));
        }

        public virtual IEnumerable<FilmeModel> Disputar()
        {
            var ganhadores = _partidas.Select(d => d.Disputar());

            return ganhadores;
        }

        public virtual IEnumerable<FilmeModel> ObterParticipantes()
        {
            return _participantes;
        }

        public virtual IEnumerable<FilmePosicaoModel> ObterParticipantesPosicao()
        {
            if (_partidas.Count() == 0)
            {
                throw new ChaveNaoMontadaException();
            }

            var filmesPosicao = new List<FilmePosicaoModel>();

            for (int i = 0; i < _partidas.Count(); i++)
            {
                var partida = _partidas.ElementAt(i);

                IEnumerable<FilmePosicaoModel> participantesPartida = partida.ObterParticipantes().Select((f, index) => new FilmePosicaoModel
                {
                    Posicao = index + 1,
                    Id = f.Id,
                    Titulo = f.Titulo,
                    Nota = f.Nota,
                    Ano = f.Ano
                });

                filmesPosicao.AddRange(participantesPartida);
            }

            return filmesPosicao;
        }
    }
}
