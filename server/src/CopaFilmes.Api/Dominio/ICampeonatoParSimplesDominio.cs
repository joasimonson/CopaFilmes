using CopaFilmes.Api.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Dominio
{
    public interface ICampeonatoParSimplesDominio
    {
        Task<IEnumerable<FilmePosicaoModel>> Disputar(string[] idsParticipantes);
    }
}
