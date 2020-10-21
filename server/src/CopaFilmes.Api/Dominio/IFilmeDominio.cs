using CopaFilmes.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Dominio
{
    public interface IFilmeDominio
    {
        public Task<IEnumerable<FilmeModel>> ObterFilmesAsync();
    }
}
