using CopaFilmes.Api.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Repositorios
{
    public interface IFilmeRepositorio
    {
        public Task<IEnumerable<Filme>> ObterFilmesAsync();
    }
}
